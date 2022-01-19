using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [ApiVersion("1")]
    internal sealed class SuppliersController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuppliersByProductId(Guid id)
        {
            var query = new GetSuppliersByProductQuery()
            {
                ProductId = id
            };

            return Ok(await Mediator.Send(query));
        }
    }
}