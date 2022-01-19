using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        public Task<Result<GetSuppliersResponse>> GetSupplierById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}