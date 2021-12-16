﻿using FluentValidation;
using Snippet.Micro.RBAC.Constants;
using Snippet.Micro.RBAC.Core;

namespace Snippet.Micro.RBAC.Models.RBAC.Element
{
    public class CreateElementInputModel
    {
        public int? UpId { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }

        public string Identity { get; set; }

        public string AccessApi { get; set; }
    }

    public class CreateElementInputModelValidator : AbstractValidator<CreateElementInputModel>
    {
        public CreateElementInputModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ConfirmMessage(MessageConstant.ELEMENT_ERROR_0001);
            RuleFor(x => x.Name).MaximumLength(50).ConfirmMessage(MessageConstant.ELEMENT_ERROR_0002);
            RuleFor(x => x.Type).NotEmpty().ConfirmMessage(MessageConstant.ELEMENT_ERROR_0003);
            RuleFor(x => x.Identity).NotEmpty().ConfirmMessage(MessageConstant.ELEMENT_ERROR_0004);
            RuleFor(x => x.Identity).MaximumLength(80).ConfirmMessage(MessageConstant.ELEMENT_ERROR_0005);
            RuleFor(x => x.Identity).Matches("^[A-Za-z0-9-_]+$").ConfirmMessage(MessageConstant.ELEMENT_ERROR_0006);
        }
    }
}