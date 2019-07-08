using Cayent.Core.CQRS.Notifications.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Notifications.Queries.Query
{
    public sealed class GetUnreadNotificationReceiversByUserIdQuery : Query<PaginatedNotificationReceiverDto>
    {
        public GetUnreadNotificationReceiversByUserIdQuery(string correlationId, string userId, 
            string criteria, int page, int pageSize, string sortBy, bool sortOrderAsc)
            : base(correlationId)
        {
            UserId = userId;
            Criteria = criteria;
            Page = page;
            PageSize = pageSize;
            SortBy = sortBy;
            SortOrderAsc = sortOrderAsc;
        }

        public string UserId { get; }
        public string Criteria { get; }
        public int Page { get; }
        public int PageSize { get; }
        public string SortBy { get; }
        public bool SortOrderAsc { get; }
    }
}
