using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Snippet.Micro.Identity.Data;
using System.Security.Claims;

namespace Snippet.Micro.Identity
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IssuedClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
