using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Role
{
    public class AddOrUpdateRoleInputModel
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 权限信息
        /// </summary>
        public int[] Rights { get; set; }
    }

    public class AddOrUpdateRoleInputModelValidator : AbstractValidator<AddOrUpdateRoleInputModel>
    {
        public AddOrUpdateRoleInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageConstant.ROLE_ERROR_0001);
            RuleFor(x => x.Name).MaximumLength(20).WithMessage(MessageConstant.ROLE_ERROR_0002);
            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageConstant.COMMON_ERROR_0001);
            RuleFor(x => x.Code).MaximumLength(40).WithMessage(MessageConstant.COMMON_ERROR_0002);
            RuleFor(x => x.Code).Matches("^[A-Za-z0-9-_]+$").WithMessage(MessageConstant.COMMON_ERROR_0003);
            RuleFor(x => x.Remark).MaximumLength(200).WithMessage(MessageConstant.ROLE_ERROR_0006); ;
        }
    }
}