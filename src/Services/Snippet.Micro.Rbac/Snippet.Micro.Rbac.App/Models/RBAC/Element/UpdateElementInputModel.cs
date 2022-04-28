using FluentValidation;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Element
{
    public class UpdateElementInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public string Identity { get; set; }

        public string AccessApi { get; set; }
    }

    public class UpdateElementInputModelValidator : AbstractValidator<UpdateElementInputModel>
    {
        public UpdateElementInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Identity).NotEmpty();
            RuleFor(x => x.Identity).MaximumLength(80);
            RuleFor(x => x.Identity).Matches("^[A-Za-z0-9-_]+$");
        }
    }
}