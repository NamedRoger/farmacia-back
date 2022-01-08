using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Suppliers.Commands;
using FluentPOS.Modules.People.Core.Features.Suppliers.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.People.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiVersion("1")]
    internal sealed class SuppliersController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Suppliers.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedSupplierFilter filter)
        {
            var request = Mapper.Map<GetSuppliersQuery>(filter);
            var suppliers = await Mediator.Send(request);
            return Ok(suppliers);
        }

        //[HttpGet]
        //[Authorize(Policy = Permissions.Suppliers.View)]
        //public async Task<IActionResult> GetByIdAsync()
        //{
        //    return Ok();
        //}

        [HttpPost]
        [Authorize(Policy = Permissions.Suppliers.Register)]
        public async Task<IActionResult> RegisterAsync(RegisteredSupplierCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Suppliers.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateSupplierCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Suppliers.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveSupplierCommand(id)));
        }
    }
}