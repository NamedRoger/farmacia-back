using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetSuppliersByProductQuery : IRequest<Result<List<GetSuppliersByProductResponse>>>
    {
        public Guid ProductId { get; set; }
    }
}