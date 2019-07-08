using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Permissions.Queries.Query
{
    public sealed class SearchPermissionsQuery : Query<PaginatedSearchedPermissionDto>
    {
        public SearchPermissionsQuery(string correlationId, string criteria, int page, int pageSize, string sortBy, bool sortOrderAsc)
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
