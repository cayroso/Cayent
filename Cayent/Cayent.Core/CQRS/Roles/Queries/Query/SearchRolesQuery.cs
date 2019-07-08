using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Roles.Queries.Query
{
    public sealed class SearchRolesQuery : Query<PaginatedSearchedRoleDto>
    {
        public SearchRolesQuery(string correlationId, string criteria, int page, int pageSize, string sortBy, bool sortOrderAsc)
            : base(correlationId)
        {
            Criteria = criteria;
            Page = page;
            PageSize = pageSize;
            SortBy = sortBy;
            SortOrderAsc = sortOrderAsc;
        }

        public string Criteria { get; }
        public int Page { get; }
        public int PageSize { get; }
        public string SortBy { get; }
        public bool SortOrderAsc { get; }
    }
}
