using FluentValidation;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Organization
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
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50);

            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Code).MaximumLength(50);
            RuleFor(x => x.Code).Matches("[A-Za-z0-9-_]+");
        }
    }
}