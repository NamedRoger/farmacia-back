using System;

namespace FluentPOS.Shared.DTOs.People.Suppliers
{
    public record GetSuppliersResponse(Guid Id, string Name, string Phone, string Email, string Company, string RFC);
}