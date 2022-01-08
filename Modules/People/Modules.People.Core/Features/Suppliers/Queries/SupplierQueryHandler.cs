using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Queries
{
    internal class SupplierQueryHandler :
        IRequestHandler<GetSuppliersQuery, PaginatedResult<GetSuppliersResponse>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SupplierQueryHandler> _localizer;

        public SupplierQueryHandler(IStringLocalizer<SupplierQueryHandler> localizer, IMapper mapper, IPeopleDbContext context)
        {
            _localizer = localizer;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedResult<GetSuppliersResponse>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Supplier, GetSuppliersResponse>> expression = e => new GetSuppliersResponse(e.Id, e.Name, e.Phone, e.Email, e.Company, e.RFC);

            var queryable = _context.Suppliers.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordeaning = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordeaning) ? queryable.OrderBy(ordeaning) : queryable.OrderBy(x => x.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(s => s.Email.Contains(request.SearchString) ||
                    s.Name.Contains(request.SearchString) ||
                    s.Company.Contains(request.SearchString) ||
                    s.Phone.Contains(request.SearchString));
            }

            var supplierList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (supplierList == null)
            {
                throw new PeopleException(_localizer["Suppliers not found"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetSuppliersResponse>>(supplierList);
        }
    }
}