using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands
{
    public class RemoveSupplierCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveSupplierCommand(Guid id)
        {
            Id = id;
        }
    }
}