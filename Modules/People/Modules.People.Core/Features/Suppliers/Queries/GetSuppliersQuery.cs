using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Suppliers;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Suppliers.Queries
{
    public class GetSuppliersQuery : IRequest<PaginatedResult<GetSuppliersResponse>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }
    }
}