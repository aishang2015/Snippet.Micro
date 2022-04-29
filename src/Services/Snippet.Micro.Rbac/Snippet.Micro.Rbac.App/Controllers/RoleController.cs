using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snippet.Micro.Common.Models;
using Snippet.Micro.Rbac.App.Constants;
using Snippet.Micro.Rbac.App.Data;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;
using Snippet.Micro.Rbac.App.Models.RBAC.Role;

namespace Snippet.Micro.Rbac.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly SnippetAdminDbContext _dbContext;

        private readonly IMapper _mapper;

        public RoleController(SnippetAdminDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 设置角色状态
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> ActiveRole([FromBody] ActiveRoleInputModel inputModel)
        {
            var role = await _dbContext.Roles.FindAsync(inputModel.Id);
            role.IsActive = inputModel.IsActive;
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.ROLE_INFO_0004);
        }

        /// <summary>
        /// 取得单个角色
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GetRoleOutputModel), 200)]
        public async Task<CommonResult> GetRole([FromBody] IdInputModel<int> inputModel)
        {
            var role = await _dbContext.Roles.FindAsync(inputModel.Id);
            var result = _mapper.Map<GetRoleOutputModel>(role);
            result.Rights = _dbContext.RoleClaims.Where(r => r.RoleId == inputModel.Id && r.ClaimType == ClaimConstant.RoleRight)
                .Select(r => int.Parse(r.ClaimValue)).ToArray();
            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 取得角色列表
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PagedOutputModel<GetRoleOutputModel>), 200)]
        public async Task<CommonResult> GetRolesAsync([FromBody] PagedInputModel inputModel)
        {
            var roles = await _dbContext.Roles.Skip(inputModel.SkipCount)
                .Take(inputModel.TakeCount).ToListAsync();

            var result = new PagedOutputModel<GetRoleOutputModel>()
            {
                Total = _dbContext.Roles.Count(),
                Data = _mapper.Map<List<GetRoleOutputModel>>(roles)
            };

            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 取得角色字典
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DicOutputModel), 200)]
        public async Task<CommonResult> GetRoleDic()
        {
            var result = await _dbContext.Roles.Select(r => new DicOutputModel
            {
                Key = r.Id,
                Value = r.Name
            }).ToListAsync();

            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 添加或更新角色信息
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> AddOrUpdateRoleAsync([FromBody] AddOrUpdateRoleInputModel inputModel)
        {
            // 校验名称和代码重复
            if (_dbContext.Roles.Any(r => r.Id != inputModel.Id && r.Name == inputModel.Name))
            {
                return CommonResultExtension.Fail(MessageConstant.ROLE_ERROR_0007);
            }
            if (_dbContext.Roles.Any(r => r.Id != inputModel.Id && r.Code == inputModel.Code))
            {
                return CommonResultExtension.Fail(MessageConstant.ROLE_ERROR_0008);
            }

            using var trans = await _dbContext.Database.BeginTransactionAsync();

            // 保存角色信息
            var role = await _dbContext.Roles.FindAsync(inputModel.Id);
            if (role != null)
            {
                _mapper.Map(inputModel, role);
                _dbContext.Roles.Update(role);
            }
            else
            {
                role = _dbContext.Roles.Add(_mapper.Map<SnippetAdminRole>(inputModel)).Entity;
            }
            await _dbContext.SaveChangesAsync();

            // 保存权限信息
            // 清理旧权限
            var roleClaims = _dbContext.RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == ClaimConstant.RoleRight)
                .ToList();
            _dbContext.RoleClaims.RemoveRange(roleClaims);

            // 保存新权限
            if (inputModel.Rights != null && inputModel.Rights.Length != 0)
            {
                inputModel.Rights.ToList().ForEach(r =>
                {
                    _dbContext.RoleClaims.Add(new Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>
                    {
                        RoleId = role.Id,
                        ClaimType = ClaimConstant.RoleRight,
                        ClaimValue = r.ToString(),
                    });
                });

                await _dbContext.SaveChangesAsync();
            }
            await trans.CommitAsync();

            return CommonResultExtension.Success(MessageConstant.ROLE_INFO_0001);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> RemoveRoleAsync([FromBody] IdInputModel<int> inputModel)
        {
            var role = await _dbContext.Roles.FindAsync(inputModel.Id);
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.ROLE_INFO_0002);
        }
    }
}