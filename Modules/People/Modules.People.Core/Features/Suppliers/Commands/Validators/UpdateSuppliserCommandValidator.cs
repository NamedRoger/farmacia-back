using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands.Validators
{
    public class UpdateSuppliserCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSuppliserCommandValidator(IStringLocalizer<UpdateSuppliserCommandValidator> localizer)
        {
            RuleFor(s => s.Id)
                  .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(s => s.Name)
               .NotEmpty();
            RuleFor(s => s.Phone)
                .NotEmpty();
        }
    }
}