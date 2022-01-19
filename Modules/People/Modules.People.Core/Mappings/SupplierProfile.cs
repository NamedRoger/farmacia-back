using System.Collections.Generic;
using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Suppliers.Commands;
using FluentPOS.Modules.People.Core.Features.Suppliers.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.People.Suppliers;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<RegisteredSupplierCommand, Supplier>().ReverseMap();
            CreateMap<UpdateSupplierCommand, Supplier>().ReverseMap();
            CreateMap<GetSuppliersResponse, Supplier>().ReverseMap();
            CreateMap<List<GetSuppliersResponse>, Supplier>().ReverseMap();
            CreateMap<PaginatedSupplierFilter, GetSuppliersQuery>()
                .ForMember(des => des.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}