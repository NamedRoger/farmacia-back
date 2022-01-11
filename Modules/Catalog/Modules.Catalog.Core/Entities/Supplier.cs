using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Supplier : BaseEntity
    {
        public Guid SupplierId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public bool ActivePrice { get; set; }
    }
}