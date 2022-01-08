using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Suppliers;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Queries.Validators
{
    public class PaginatedSupplierFilterValidator : PaginatedFilterValidator<Guid, Supplier, PaginatedSupplierFilter>
    {
        public PaginatedSupplierFilterValidator(IStringLocalizer<PaginatedSupplierFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}