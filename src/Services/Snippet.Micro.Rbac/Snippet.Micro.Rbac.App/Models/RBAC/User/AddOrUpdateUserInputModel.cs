using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class AddOrUpdateUserInputModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public int[] Roles { get; set; }
    }

    public class AddOrUpdateUserInputModelValidator : AbstractValidator<AddOrUpdateUserInputModel>
    {
        public AddOrUpdateUserInputModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(MessageConstant.USER_ERROR_0002);
            RuleFor(x => x.UserName).MaximumLength(20).WithMessage(MessageConstant.USER_ERROR_0003);
            RuleFor(x => x.UserName).Matches("^[A-Za-z0-9]+$").WithMessage(MessageConstant.USER_ERROR_0004);

            RuleFor(x => x.RealName).NotEmpty().WithMessage(MessageConstant.USER_ERROR_0005);
            RuleFor(x => x.RealName).MaximumLength(20).WithMessage(MessageConstant.USER_ERROR_0006);
        }
    }
}