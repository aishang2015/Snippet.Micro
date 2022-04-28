using FluentValidation;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Organization
{
    public class UpdateOrganizationInputModel
    {
        public int? UpId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Icon { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

    public class UpdateOrganizationInputModelValidator : AbstractValidator<UpdateOrganizationInputModel>
    {
        public UpdateOrganizationInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50);
        }
    }
}