using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public class GetSuppliersByProductResponse
    {
        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }

        public string SupplierName { get; set; }

        public int TypedConversion { get; set; }

        public string Conversion { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public bool IsPriceActive { get; set; }

        public string FileName { get; set; }
    }
}