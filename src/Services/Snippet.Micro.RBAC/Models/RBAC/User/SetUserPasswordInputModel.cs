using FluentValidation;
using Snippet.Micro.RBAC.Constants;
using Snippet.Micro.RBAC.Core;

namespace Snippet.Micro.RBAC.Models.RBAC.User
{
    public class SetUserPasswordInputModel
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class SetUserPasswordInputModelValidator : AbstractValidator<SetUserPasswordInputModel>
    {
        public SetUserPasswordInputModelValidator()
        {
            RuleFor(x => x.Password).NotEmpty().ConfirmMessage(MessageConstant.USER_ERROR_0007);
            RuleFor(x => x.Password).MaximumLength(20).ConfirmMessage(MessageConstant.USER_ERROR_0008);

            RuleFor(x => x.ConfirmPassword).NotEmpty().ConfirmMessage(MessageConstant.USER_ERROR_0009);
        }
    }
}