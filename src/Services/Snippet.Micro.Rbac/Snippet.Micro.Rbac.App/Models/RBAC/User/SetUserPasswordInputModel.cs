using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class SetUserPasswordInputModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认新密码
        /// </summary>
        public string ConfirmPassword { get; set; }
    }

    public class SetUserPasswordInputModelValidator : AbstractValidator<SetUserPasswordInputModel>
    {
        public SetUserPasswordInputModelValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage(MessageConstant.USER_ERROR_0007);
            RuleFor(x => x.Password).MaximumLength(20).WithMessage(MessageConstant.USER_ERROR_0008);

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(MessageConstant.USER_ERROR_0009);
        }
    }
}