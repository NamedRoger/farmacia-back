using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.People
{
    public interface ISupplierService
    {
        public Task<Result<GetSuppliersResponse>> GetSupplierById(Guid id);
    }
}