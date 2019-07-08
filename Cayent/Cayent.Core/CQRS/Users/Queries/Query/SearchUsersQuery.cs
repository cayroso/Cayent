using Cayent.Core.CQRS.Users.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Users.Queries.Query
{
    public sealed class SearchUsersQuery : Query<PaginatedSearchedUserDto>
    {
        public SearchUsersQuery(string correlationId, string criteria, int page, int pageSize, string sortBy, bool sortOrderAsc)
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
