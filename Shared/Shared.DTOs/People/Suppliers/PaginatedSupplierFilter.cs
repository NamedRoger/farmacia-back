// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedSupplierFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.Suppliers
{
    public class PaginatedSupplierFilter : PaginatedFilter
    {
        public string SearchString { get; set; }
    }
}