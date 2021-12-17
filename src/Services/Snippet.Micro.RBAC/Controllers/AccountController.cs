using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.RBAC.Constants;
using Snippet.Micro.RBAC.Core.Attribute;
using Snippet.Micro.RBAC.Core.UserAccessor;
using Snippet.Micro.RBAC.Data;
using Snippet.Micro.RBAC.Data.Entity.RBAC;
using Snippet.Micro.RBAC.Models;
using Snippet.Micro.RBAC.Models.RBAC.Account;

namespace Snippet.Micro.RBAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 获取当前用户的信息
        /// </summary>
        [Authorize]
        [HttpPost("GetCurrentUserInfo")]
        [CommonResultResponseType(typeof(CommonResult<GetCurrentUserInfoOutputModel>))]
        public async Task<CommonResult> GetCurrentUserInfo(
            [FromServices] IUserAccessor userAccessor,
            [FromServices] UserManager<SnippetAdminUser> userManager,
            [FromServices] SnippetAdminDbContext dbContext)
        {
            var user = await userManager.FindByNameAsync(userAccessor.UserName);
            var roles = await userManager.GetRolesAsync(user);
            var elementIds = (from role in dbContext.Roles
                              from element in dbContext.Elements
                              from rc in dbContext.RoleClaims
                              where
                                 role.IsActive &&
                                 element.Id.ToString() == rc.ClaimValue &&
                                 rc.ClaimType == ClaimConstant.RoleRight &&
                                 rc.RoleId == role.Id &&
                                 roles.Contains(role.Name)
                              select element.Id).Distinct().ToList();

            var identities = (from element in dbContext.Elements
                              from elementTree in dbContext.ElementTrees
                              where element.Id == elementTree.Ancestor &&
                                    elementIds.Contains(elementTree.Descendant)
                              select element.Identity).Distinct().ToArray();

            return this.SuccessCommonResult(new GetCurrentUserInfoOutputModel
            {
                UserName = user.RealName,
                Identities = identities
            });
        }
    }
}
