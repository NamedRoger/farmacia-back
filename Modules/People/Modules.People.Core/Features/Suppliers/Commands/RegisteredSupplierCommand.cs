using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands
{
    public class RegisteredSupplierCommand : IRequest<Result<Guid>>
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string RFC { get; set; }

        public string Company { get; set; }

        public string FileName { get; set; }
    }
}