using FluentValidation;
using Snippet.Micro.Rbac.App.Constants;

namespace Snippet.Micro.Rbac.App.Models.RBAC.Organization
{
    public class UpdateOrganizationInputModel
    {
        /// <summary>
        /// 上级组织id
        /// </summary>
        public int? UpId { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// 组织图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 组织电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 组织地址
        /// </summary>
        public string Address { get; set; }
    }

    public class UpdateOrganizationInputModelValidator : AbstractValidator<UpdateOrganizationInputModel>
    {
        public UpdateOrganizationInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(MessageConstant.ORGANIZATION_ERROR_0001);
            RuleFor(x => x.Name).MaximumLength(50).WithMessage(MessageConstant.ORGANIZATION_ERROR_0002);

            RuleFor(x => x.Code).NotEmpty().WithMessage(MessageConstant.COMMON_ERROR_0001);
            RuleFor(x => x.Code).MaximumLength(50).WithMessage(MessageConstant.COMMON_ERROR_0002);
            RuleFor(x => x.Code).Matches("[A-Za-z0-9-_]+").WithMessage(MessageConstant.COMMON_ERROR_0003);
        }
    }
}