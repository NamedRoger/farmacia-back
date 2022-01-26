// --------------------------------------------------------------------------------------------------
// <copyright file="ProductQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    internal class ProductQueryHandler :
        IRequestHandler<GetProductsQuery, PaginatedResult<GetProductsResponse>>,
        IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>,
        IRequestHandler<GetSuppliersByProductQuery, Result<List<GetSuppliersByProductResponse>>>,
        IRequestHandler<GetProductImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IStringLocalizer<ProductQueryHandler> _localizer;
        private readonly ISupplierService _supplierService;

        public ProductQueryHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IOptions<ApplicationSettings> applicationSettings,
            IStringLocalizer<ProductQueryHandler> localizer,
            ISupplierService supplierService)
        {
            _context = context;
            _mapper = mapper;
            _applicationSettings = applicationSettings.Value;
            _localizer = localizer;
            _supplierService = supplierService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var queryable = _context.Products.AsNoTracking()
                .ProjectTo<GetProductsResponse>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Id)
                .AsQueryable();

            if (request.BrandIds.Any())
            {
                queryable = queryable.Where(x => request.BrandIds.Contains(x.BrandId));
            }

            if (request.CategoryIds.Any())
            {
                queryable = queryable.Where(x => request.CategoryIds.Contains(x.CategoryId));
            }

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.LocaleName.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.BarcodeSymbology.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var productList = await queryable
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (productList == null)
            {
                throw new CatalogException(_localizer["Products Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetProductsResponse>>(productList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var product = await _context.Products.AsNoTracking()
                .Where(p => p.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);
            return await Result<GetProductByIdResponse>.SuccessAsync(mappedProduct);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(GetProductImageQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            string data = await _context.Products.AsNoTracking()
                .Where(p => p.Id == query.Id)
                .Select(x => x.ImageUrl)
                .FirstOrDefaultAsync(cancellationToken);
            data = data ?? "Files\\Images\\Catalog\\Products\\default.png";

            return await Result<string>.SuccessAsync(data: $"{_applicationSettings.ApiUrl}{data.Replace(@"\", "/")}");
        }

        public async Task<Result<List<GetSuppliersByProductResponse>>> Handle(GetSuppliersByProductQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers.AsNoTracking()
                 .Where(s => s.ProductId == request.ProductId)
                 .ToListAsync();
            List<GetSuppliersByProductResponse> suppliersMaped = new List<GetSuppliersByProductResponse>();
            foreach (var s in suppliers)
            {
                var supplier = await _supplierService.GetSupplierById(s.SupplierId);
                suppliersMaped.Add(new GetSuppliersByProductResponse
                {
                    Cost = s.Cost,
                    IsPriceActive = s.ActivePrice,
                    Price = s.Price,
                    ProductId = request.ProductId,
                    SupplierId = s.Id,
                    SupplierName = supplier.Data.Name,
                    Conversion = s.Conversion,
                    TypedConversion = (int)s.TypeConversion,
                    FileName = s.FileName
                });
            }

            return await Result<List<GetSuppliersByProductResponse>>.SuccessAsync(suppliersMaped);
        }
    }
}