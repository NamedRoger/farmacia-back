using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class RemoveSupplierProductCommand : IRequest<Result<Guid>>
    {
        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }

        public RemoveSupplierProductCommand()
        {
        }

        public RemoveSupplierProductCommand(Guid productId, Guid supplierId)
        {
            ProductId = productId;
            SupplierId = supplierId;
        }
    }
}