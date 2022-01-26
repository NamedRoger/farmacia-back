using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.People.Suppliers;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Queries
{
    public class GetSuppliersByProductIdQuery : IRequest<Result<List<GetSuppliersResponse>>>
    {
        public Guid ProductId { get; set; }
    }
}