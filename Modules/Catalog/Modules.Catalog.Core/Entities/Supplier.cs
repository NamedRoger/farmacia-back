using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Enums;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Supplier : BaseEntity
    {
        public Guid SupplierId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public string FileName { get; set; }

        public string Conversion { get; set; }

        public TypeConversionPriceProduct TypeConversion { get; set; } = TypeConversionPriceProduct.Percentage;

        public bool ActivePrice { get; set; }

        public void CalculatePrice()
        {
            decimal conversionTyped;
            switch (TypeConversion)
            {
                case TypeConversionPriceProduct.Percentage:
                    conversionTyped = decimal.Parse(Conversion);
                    Price = (Cost * conversionTyped) + Cost;
                    break;
                case TypeConversionPriceProduct.FixedPrice:
                    conversionTyped = decimal.Parse(Conversion);
                    break;
                default:
                    conversionTyped = decimal.Parse(Conversion);
                    Price = (Cost * conversionTyped) + Cost;
                    break;
            }
        }
    }
}