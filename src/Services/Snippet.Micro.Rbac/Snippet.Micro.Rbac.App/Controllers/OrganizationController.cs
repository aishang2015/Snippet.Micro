using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snippet.Micro.Common.Models;
using Snippet.Micro.Rbac.App.Constants;
using Snippet.Micro.Rbac.App.Data;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;
using Snippet.Micro.Rbac.App.Models.RBAC.Organization;

namespace Snippet.Micro.RBAC.Controllers.RBAC
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly SnippetAdminDbContext _dbContext;

        public OrganizationController(SnippetAdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取组织机构详细信息
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GetOrganizationOutputModel), 200)]
        public async Task<CommonResult> GetOrganization([FromBody] IdInputModel<int> inputModel,
            [FromServices] IMapper mapper)
        {
            var org = await _dbContext.Organizations.FindAsync(inputModel.Id);
            var result = mapper.Map<GetOrganizationOutputModel>(org);

            result.UpId = (from tree in _dbContext.OrganizationTrees
                           where tree.Descendant == inputModel.Id && tree.Length == 1
                           select tree).FirstOrDefault()?.Ancestor;
            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 获取组织树信息
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(List<GetOrganizationTreeOutputModel>), 200)]
        public async Task<CommonResult> GetOrganizationTree()
        {
            var orgs = await _dbContext.Organizations.ToListAsync();
            var orgTrees = await _dbContext.OrganizationTrees.ToListAsync();

            // 找到最上层,即只做为自己的子节点
            var topOrgKeys = from ot in orgTrees
                             group ot by ot.Descendant into otg
                             where otg.Count() == 1
                             select otg.Key;
            var topOrgs = (from org in orgs
                           where topOrgKeys.Contains(org.Id)
                           select org).ToList();

            // 递归生成树数据
            var result = MakeTreeData(orgs, orgTrees, topOrgs);
            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(List<GetOrganizationTreeOutputModel>), 200)]
        public async Task<CommonResult> CreateOrganization([FromBody] CreateOrganizationInputModel inputModel,
            [FromServices] IMapper mapper)
        {
            // 校验组织编码重复
            if (_dbContext.Organizations.Any(o => o.Code == inputModel.Code))
            {
                return CommonResultExtension.Fail(MessageConstant.ORGANIZATION_ERROR_0004);
            }

            // 开启事务
            using var tran = await _dbContext.Database.BeginTransactionAsync();

            // 保存节点
            var entity = await _dbContext.Organizations.AddAsync(mapper.Map<Organization>(inputModel));
            await _dbContext.SaveChangesAsync();

            // 保存节点关系
            var treeData = _dbContext.OrganizationTrees.Where(t => t.Descendant == inputModel.UpId);
            foreach (var treeNode in treeData)
            {
                await _dbContext.OrganizationTrees.AddAsync(new OrganizationTree
                {
                    Ancestor = treeNode.Ancestor,
                    Descendant = entity.Entity.Id,
                    Length = treeNode.Length + 1
                });
            }
            await _dbContext.OrganizationTrees.AddAsync(new OrganizationTree
            {
                Ancestor = entity.Entity.Id,
                Descendant = entity.Entity.Id,
                Length = 0
            });
            await _dbContext.SaveChangesAsync();
            await tran.CommitAsync();
            return CommonResultExtension.Success(MessageConstant.ORGANIZATION_INFO_0001);
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> DeleteOrganization([FromBody] IdInputModel<int> inputModel)
        {
            var organizations = from org in _dbContext.Organizations
                                join orgTree in _dbContext.OrganizationTrees on org.Id equals orgTree.Descendant
                                where orgTree.Ancestor == inputModel.Id
                                select org;
            _dbContext.Organizations.RemoveRange(organizations);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.ORGANIZATION_INFO_0002);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> UpdateOrganization([FromBody] UpdateOrganizationInputModel inputModel,
            [FromServices] IMapper mapper)
        {
            // 校验组织编码重复
            if (_dbContext.Organizations.Any(o => o.Code == inputModel.Code && o.Id != inputModel.Id))
            {
                return CommonResultExtension.Fail(MessageConstant.ORGANIZATION_ERROR_0004);
            }

            // 判断树节点是否被移动
            var upNode = (from orgTree in _dbContext.OrganizationTrees
                          where orgTree.Descendant == inputModel.Id &&
                               orgTree.Length == 1
                          select orgTree).FirstOrDefault();

            // 父节点变化了
            if (upNode?.Ancestor != inputModel.UpId)
            {
                // 找到上一步祖先节点和后代节点之间的所有关系
                var treeNodes = from orgTree in _dbContext.OrganizationTrees
                                let parentNodeIds = (from parentTree in _dbContext.OrganizationTrees
                                                     where parentTree.Descendant == inputModel.Id && parentTree.Length > 0
                                                     select parentTree.Ancestor).ToList()
                                let childNodeIds = (from childTree in _dbContext.OrganizationTrees
                                                    where childTree.Ancestor == inputModel.Id
                                                    select childTree.Descendant).ToList()
                                where parentNodeIds.Contains(orgTree.Ancestor) && childNodeIds.Contains(orgTree.Descendant)
                                select orgTree;
                _dbContext.RemoveRange(treeNodes);

                // 当前文件夹的子树
                var childNodes = (from ft in _dbContext.OrganizationTrees
                                  where ft.Ancestor == inputModel.Id
                                  select ft).ToList();

                // 找到新父节点的全部祖先
                var newParents = (from ft in _dbContext.OrganizationTrees
                                  where ft.Descendant == inputModel.UpId
                                  select ft).ToList();

                // 笛卡尔积构造新的上级树和子树的关系
                foreach (var parent in newParents)
                {
                    foreach (var node in childNodes)
                    {
                        await _dbContext.OrganizationTrees.AddAsync(new OrganizationTree
                        {
                            Ancestor = parent.Ancestor,
                            Descendant = node.Descendant,
                            Length = parent.Length + node.Length + 1
                        });
                    }
                }
            }

            var organization = mapper.Map<Organization>(inputModel);
            _dbContext.Organizations.Update(organization);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.ORGANIZATION_INFO_0003);
        }

        /// <summary>
        /// 生成树数据
        /// </summary>
        private List<GetOrganizationTreeOutputModel> MakeTreeData(
            List<Organization> orgs,
            List<OrganizationTree> orgTrees,
            List<Organization> childOrgs)
        {
            var outputModels = childOrgs.Select(o => new GetOrganizationTreeOutputModel
            {
                Title = o.Name,
                Id = o.Id,
                Icon = o.Icon,
                Children = new List<GetOrganizationTreeOutputModel>()
            }).ToList();

            // 循环每个节点，找到子节点
            foreach (var node in outputModels)
            {
                // 当前节点的子节点
                var childs = (from org in orgs
                              join orgTree in orgTrees on org.Id equals orgTree.Descendant
                              where orgTree.Length == 1 && orgTree.Ancestor == node.Id
                              select org).ToList();
                node.Children.AddRange(MakeTreeData(orgs, orgTrees, childs));
            }

            return outputModels;
        }
    }
}