using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.User
{
    public class AddOrgMemberInputModel
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int[] UserIds { get; set; }
    }

    public class AddOrgMemberInputModelValidator : AbstractValidator<AddOrgMemberInputModel>
    {
        public AddOrgMemberInputModelValidator()
        {
            RuleFor(x => x.UserIds).NotNull().WithMessage(MessageConstant.ORGANIZATION_ERROR_0007);
            RuleFor(x => x.UserIds).Must(p => p.Length > 0).WithMessage(MessageConstant.ORGANIZATION_ERROR_0007);
        }
    }
}