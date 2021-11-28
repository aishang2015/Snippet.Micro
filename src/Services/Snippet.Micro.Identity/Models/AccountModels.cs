using System.ComponentModel.DataAnnotations;

namespace Snippet.Micro.Identity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入账号！")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入密码！")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LogoutViewModel
    {
        public string PostLogoutRedirectUri { get; set; }

        public string LogoutId { get; set; }
    }
}
