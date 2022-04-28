using FluentValidation;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Role
{
    public class AddOrUpdateRoleInputModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Remark { get; set; }

        public int[] Rights { get; set; }
    }

    public class AddOrUpdateRoleInputModelValidator : AbstractValidator<AddOrUpdateRoleInputModel>
    {
        public AddOrUpdateRoleInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(20);
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Code).MaximumLength(40);
            RuleFor(x => x.Code).Matches("^[A-Za-z0-9-_]+$");
            RuleFor(x => x.Remark).MaximumLength(200);
        }
    }
}