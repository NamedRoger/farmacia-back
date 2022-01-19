using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IRequest<Result<GetSuppliersResponse>>
    {
        public Guid SupplierId { get; set; }
    }
}