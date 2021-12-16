﻿using Microsoft.AspNetCore.Mvc.Filters;
using Snippet.Micro.RBAC.Core.UserAccessor;

namespace Snippet.Micro.RBAC.Data.Auth
{
    public class SnippetAdminAuthorizeFilter : IAuthorizationFilter
    {
        private readonly SnippetAdminDbContext _dbContext;

        private readonly IUserAccessor _userAccessor;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SnippetAdminAuthorizeFilter(
            SnippetAdminDbContext dbContext,
            IUserAccessor userAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //// user not exist or is not actived
            //if (!_dbContext.CacheSet<SnippetAdminUser>().Any(u => u.UserName == _userAccessor.UserName && u.IsActive))
            //{
            //    context.Result = new StatusCodeResult(403);
            //    return;
            //}

            //// get all user role
            //var userId = _dbContext.CacheSet<SnippetAdminUser>().First(u => u.UserName == _userAccessor.UserName).Id;
            //var userRoles = _dbContext.CacheSet<IdentityUserRole<int>>().Where(ur => ur.UserId == userId);
            //if (userRoles == null || !userRoles.Any())
            //{
            //    context.Result = new StatusCodeResult(403);
            //    return;
            //}

            //// get actived roles
            //var roleIds = _dbContext.CacheSet<SnippetAdminRole>()
            //     .Where(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id) && r.IsActive)
            //     .Select(r => r.Id);

            //// get role elements id
            //var elementIds = _dbContext.CacheSet<IdentityRoleClaim<int>>()
            //    .Where(rc => rc.ClaimType == ClaimConstant.RoleRight && roleIds.Contains(rc.RoleId))
            //    .Select(rc => int.Parse(rc.ClaimValue));

            //// get all api that could access
            //var apiList = _dbContext.CacheSet<Element>()
            //    .Where(e => elementIds.Contains(e.Id))
            //    .Select(e => e.AccessApi.ToLower().Split(",").ToList())
            //    .SelectMany(e => e)
            //    .Distinct();

            //// check have right to access this api
            //var path = _httpContextAccessor.HttpContext.Request.Path.Value
            //    ?.TrimStart('/').ToLower();

            //if (!apiList.Contains(path))
            //{
            //    context.Result = new StatusCodeResult(403);
            //}
        }
    }
}