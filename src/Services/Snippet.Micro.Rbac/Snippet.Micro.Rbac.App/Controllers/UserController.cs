using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.Common.Extensions;
using Snippet.Micro.Common.Models;
using Snippet.Micro.Rbac.App.Constants;
using Snippet.Micro.Rbac.App.Data;
using Snippet.Micro.Rbac.App.Data.Entity.RBAC;
using Snippet.Micro.Rbac.App.Models.RBAC.User;

namespace Snippet.Micro.Rbac.App.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SnippetAdminDbContext _dbContext;

        private readonly IMapper _mapper;

        public UserController(SnippetAdminDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> ActiveUserAsync([FromBody] ActiveUserInputModel inputModel)
        {
            var user = await _dbContext.Users.FindAsync(inputModel.Id);
            user.IsActive = inputModel.IsActive;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0001);
        }

        /// <summary>
        /// 查找用户信息
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GetUserOutputModel), 200)]
        public async Task<CommonResult> GetUserAsync([FromBody] IdInputModel<int> inputModel)
        {
            var user = await _dbContext.Users.FindAsync(inputModel.Id);
            var result = _mapper.Map<GetUserOutputModel>(user);
            result.Roles = (from ur in _dbContext.UserRoles
                            where ur.UserId == inputModel.Id
                            select ur.RoleId).ToArray();
            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult<PagedOutputModel<SearchUserOutputModel>>), 200)]
        public CommonResult SearchUser([FromBody] SearchUserInputModel inputModel)
        {
            // 普通条件
            var query = _dbContext.Users
                .AndIfExist(inputModel.UserName, u => u.UserName.Contains(inputModel.UserName))
                .AndIfExist(inputModel.RealName, u => u.RealName.Contains(inputModel.RealName))
                .AndIfExist(inputModel.Phone, u => u.PhoneNumber.Contains(inputModel.Phone));

            // 角色过滤
            if (inputModel.Role != null)
            {
                query = from u in query
                        join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                        where ur.RoleId == inputModel.Role
                        select u;
            }

            // 组织过滤
            if (inputModel.Org != null)
            {
                query = from u in query
                        where _dbContext.UserClaims.Any(uc =>
                            uc.UserId == u.Id &&
                            uc.ClaimValue == inputModel.Org.ToString() &&
                            uc.ClaimType == ClaimConstant.UserOrg)
                        select u;
            }

            // 查询数据
            var resultQuery = from u in query
                              select new SearchUserOutputModel
                              {
                                  Id = u.Id,
                                  Avatar = u.Avatar,
                                  Gender = (int)u.Gender,
                                  IsActive = u.IsActive,
                                  PhoneNumber = u.PhoneNumber,
                                  RealName = u.RealName,
                                  UserName = u.UserName,
                                  UserRoles = (from ur in _dbContext.UserRoles
                                               join r in _dbContext.Roles on ur.RoleId equals r.Id
                                               where ur.UserId == u.Id
                                               select new RoleInfo
                                               {
                                                   RoleName = r.Name,
                                                   IsActive = r.IsActive
                                               }).ToArray(),
                                  UserOrgs = (from uc in _dbContext.UserClaims
                                              join o in _dbContext.Organizations on uc.ClaimValue equals o.Id.ToString()
                                              where uc.UserId == u.Id && uc.ClaimType == ClaimConstant.UserOrg
                                              select new UserOrg
                                              {
                                                  OrgId = o.Id,
                                                  OrgName = o.Name
                                              }).ToArray()
                              };

            var result = new PagedOutputModel<SearchUserOutputModel>
            {
                Total = resultQuery.Count(),
                Data = resultQuery.Skip(inputModel.SkipCount).Take(inputModel.TakeCount)
            };

            return CommonResultExtension.SuccessResult(result);
        }

        /// <summary>
        /// 添加或更新用户
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> AddOrUpdateUserAsync([FromBody] AddOrUpdateUserInputModel inputModel,
            [FromServices] UserManager<SnippetAdminUser> userManager)
        {
            if (_dbContext.Users.Any(u => u.UserName == inputModel.UserName && u.Id != inputModel.Id))
            {
                return CommonResultExtension.Fail("");
            }

            using var trans = await _dbContext.Database.BeginTransactionAsync();

            if (inputModel.Id != null)
            {
                var user = _dbContext.Users.Find(inputModel.Id);
                _mapper.Map(inputModel, user);
                _dbContext.Users.Update(user);

                var ur = _dbContext.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
                _dbContext.UserRoles.RemoveRange(ur);
                foreach (var role in inputModel.Roles)
                {
                    _dbContext.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role });
                }
            }
            else
            {
                var user = _mapper.Map<SnippetAdminUser>(inputModel);
                await userManager.CreateAsync(user);
                await userManager.AddPasswordAsync(user, "123456");
                if (inputModel.Roles != null)
                {
                    foreach (var role in inputModel.Roles)
                    {
                        _dbContext.UserRoles.Add(new IdentityUserRole<int> { UserId = user.Id, RoleId = role });
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
            await trans.CommitAsync();
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0001);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> RemoveUserAsync([FromBody] IdInputModel<int> inputModel)
        {
            var user = _dbContext.Users.Find(inputModel.Id);
            var uops = _dbContext.UserClaims.Where(c => c.UserId == inputModel.Id &&
                c.ClaimType == ClaimConstant.UserOrg).ToList();
            _dbContext.Remove(user);
            _dbContext.RemoveRange(uops);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0002);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> SetUserPasswordAsync([FromBody] SetUserPasswordInputModel inputModel,
            [FromServices] UserManager<SnippetAdminUser> userManager)
        {
            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return CommonResultExtension.Fail("");
            }

            var user = _dbContext.Users.Find(inputModel.Id);
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, inputModel.Password);
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0003);
        }

        /// <summary>
        /// 添加组织成员
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> AddOrgMemberAsync([FromBody] AddOrgMemberInputModel inputModel)
        {
            foreach (var user in inputModel.UserIds)
            {
                _dbContext.UserClaims.Add(new IdentityUserClaim<int>
                {
                    UserId = user,
                    ClaimType = ClaimConstant.UserOrg,
                    ClaimValue = inputModel.OrgId.ToString(),
                });
            }
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0004);
        }

        /// <summary>
        /// 删除组织成员
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CommonResult), 200)]
        public async Task<CommonResult> RemoveOrgMemberAsync([FromBody] RemoveOrgMemberInputModel inputModel)
        {
            var uops = _dbContext.UserClaims.Where(uc =>
                uc.ClaimValue == inputModel.OrgId.ToString() &&
                uc.ClaimType == ClaimConstant.UserOrg &&
                uc.UserId == inputModel.UserId).ToList();
            _dbContext.UserClaims.RemoveRange(uops);
            await _dbContext.SaveChangesAsync();
            return CommonResultExtension.Success(MessageConstant.USER_INFO_0005);
        }
    }
}