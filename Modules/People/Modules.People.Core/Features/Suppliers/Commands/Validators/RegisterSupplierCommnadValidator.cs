using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands.Validators
{
    public class RegisterSupplierCommnadValidator : AbstractValidator<RegisteredSupplierCommand>
    {
        public RegisterSupplierCommnadValidator(IStringLocalizer<RegisterSupplierCommnadValidator> _localizer)
        {
            RuleFor(s => s.Name)
                .NotEmpty();
            RuleFor(s => s.Phone)
                .NotEmpty();
        }
    }
}