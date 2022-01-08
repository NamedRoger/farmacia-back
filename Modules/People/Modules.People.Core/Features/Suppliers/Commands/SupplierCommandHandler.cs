using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Commands
{
    internal class SupplierCommandHandler :
        IRequestHandler<RegisteredSupplierCommand, Result<Guid>>,
        IRequestHandler<UpdateSupplierCommand, Result<Guid>>,
        IRequestHandler<RemoveSupplierCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        private readonly IPeopleDbContext _context;
        private readonly IStringLocalizer<SupplierCommandHandler> _localizer;

        public SupplierCommandHandler(
            IMapper mapper,
            IDistributedCache cache,
            IPeopleDbContext context,
            IStringLocalizer<SupplierCommandHandler> localizer)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(RegisteredSupplierCommand command, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Supplier>(command);
            await _context.Suppliers.AddAsync(supplier, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return await Result<Guid>.SuccessAsync(supplier.Id, _localizer["Supplier Saved"]);
        }

        public async Task<Result<Guid>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.Where(s => s.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (supplier != null)
            {
                supplier = _mapper.Map<Supplier>(request);

                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Supplier>(request.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(supplier.Id, _localizer["Supplier update"]);
            }
            else
            {
                throw new PeopleException(_localizer["Supplier not found"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(RemoveSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Supplier>(request.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(supplier.Id, _localizer["Supplier Deleted"]);
        }
    }
}