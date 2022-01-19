using System.Collections.Generic;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string RFC { get; set; }

        public string FileName { get; set; }

        public virtual ICollection<SupplierExtendenAttribute> ExtendedAttributes { get; set; }

        public Supplier()
        {
            ExtendedAttributes = new HashSet<SupplierExtendenAttribute>();
        }
    }
}