using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snippet.Micro.Identity.Data;
using Snippet.Micro.Identity.Models;

namespace Snippet.Micro.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        private readonly IClientStore _clientStore;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Account);

                if (user != null)
                {
                    var isRightPassword = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (isRightPassword)
                    {
                        // 设置认证相关属性
                        var props = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                            IsPersistent = true
                        };
                        await _signInManager.SignInAsync(user, props);

                        // 确保返回url依然可用
                        if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                    }
                }
                ModelState.AddModelError("", "错误的用户名或密码!");

            }
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogOutViewModelAsync(logoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();
            }

            return View(vm);
        }

        /// <summary>
        /// 构建登录页面所需要的信息
        /// </summary>
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            // 配置第三方登录参照identityServer4例子中的同名方法

            return new LoginViewModel
            {
                Account = context?.LoginHint,
                ReturnUrl = returnUrl,
            };
        }

        /// <summary>
        /// 构建登录页面所需要的信息
        /// </summary>
        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Account = model.Account;
            return vm;
        }

        /// <summary>
        /// 构建登出页面所需要的信息
        /// </summary>
        private async Task<LogoutViewModel> BuildLogOutViewModelAsync(string LogoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(LogoutId);

            var client = await _clientStore.FindClientByIdAsync(logout?.ClientId);
            var vm = new LogoutViewModel
            {
                PostLogoutRedirectUri = client?.PostLogoutRedirectUris?.First(),
                LogoutId = LogoutId
            };
            return vm;
        }
    }
}
