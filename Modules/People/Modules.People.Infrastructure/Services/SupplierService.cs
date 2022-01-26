using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Suppliers.Queries;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;
using MediatR;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMediator _mediator;

        public SupplierService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Result<GetSuppliersResponse>> GetSupplierById(Guid id)
        {
            return _mediator.Send(new GetSupplierByIdQuery(id));
        }
    }
}