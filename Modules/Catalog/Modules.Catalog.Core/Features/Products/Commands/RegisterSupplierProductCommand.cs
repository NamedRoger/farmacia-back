using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    public class RegisterSupplierProductCommand : IRequest<Result<Guid>>
    {
        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }

        public decimal Cost { get; set; }

        public string Conversion { get; set; }

        public int TypeConversion { get; set; }
    }
}