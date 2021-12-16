using FluentValidation;
using Snippet.Micro.RBAC.Constants;
using Snippet.Micro.RBAC.Core;

namespace Snippet.Micro.RBAC.Models.RBAC.Organization
{
    public class CreateOrganizationInputModel
    {
        public int? UpId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Icon { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class CreateOrganizationInputModelValidator : AbstractValidator<CreateOrganizationInputModel>
    {
        public CreateOrganizationInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ConfirmMessage(MessageConstant.ORGANIZATION_ERROR_0001);
            RuleFor(x => x.Name).MaximumLength(50).ConfirmMessage(MessageConstant.ORGANIZATION_ERROR_0002);

            RuleFor(x => x.Code).NotEmpty().ConfirmMessage(MessageConstant.ORGANIZATION_ERROR_0001);
            RuleFor(x => x.Code).MaximumLength(50).ConfirmMessage(MessageConstant.ORGANIZATION_ERROR_0002);
            RuleFor(x => x.Code).Matches("[A-Za-z0-9-_]+").ConfirmMessage(MessageConstant.ORGANIZATION_ERROR_0002);
        }
    }
}