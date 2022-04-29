using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Element
{
    public class CreateElementInputModel
    {
        /// <summary>
        /// 上级元素id
        /// </summary>
        public int? UpId { get; set; }

        /// <summary>
        /// 元素名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元素类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 元素标识
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 访问的api
        /// </summary>
        public string AccessApi { get; set; }
    }

    public class CreateElementInputModelValidator : AbstractValidator<CreateElementInputModel>
    {
        public CreateElementInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageConstant.ELEMENT_ERROR_0001);
            RuleFor(x => x.Name).MaximumLength(50).WithMessage(MessageConstant.ELEMENT_ERROR_0002);
            RuleFor(x => x.Type).NotEmpty().WithMessage(MessageConstant.ELEMENT_ERROR_0003);
            RuleFor(x => x.Identity).NotEmpty().WithMessage(MessageConstant.ELEMENT_ERROR_0004);
            RuleFor(x => x.Identity).MaximumLength(80).WithMessage(MessageConstant.ELEMENT_ERROR_0005);
            RuleFor(x => x.Identity).Matches("^[A-Za-z0-9-_]+$").WithMessage(MessageConstant.ELEMENT_ERROR_0006);
        }
    }
}