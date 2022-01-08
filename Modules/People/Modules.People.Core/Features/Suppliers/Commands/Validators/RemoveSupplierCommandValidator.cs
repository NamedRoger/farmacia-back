using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands.Validators
{
    public class RemoveSupplierCommandValidator : AbstractValidator<RemoveSupplierCommand>
    {
        public RemoveSupplierCommandValidator(IStringLocalizer<RemoveSupplierCommandValidator> localizer)
        {
            RuleFor(s => s.Id)
               .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}