using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Notifications.Dtos;
using Cayent.Core.CQRS.Notifications.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.Services;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.CQRS.Notifications.Queries.Handler
{
    public sealed class NotificationQueryHandler : BaseQueryHandler,
        IQueryHandler<SearchNotificationReceiversByUserIdQuery, PaginatedNotificationReceiverDto>,
        IQueryHandler<GetUnreadNotificationReceiversByUserIdQuery, PaginatedNotificationReceiverDto>

    {
        public NotificationQueryHandler(IUnitOfWorkFactory  unitOfWorkFactory) 
            : base(unitOfWorkFactory)
        {
        }

        PaginatedNotificationReceiverDto IQueryHandler<SearchNotificationReceiversByUserIdQuery, PaginatedNotificationReceiverDto>.Handle(SearchNotificationReceiversByUserIdQuery query)
        {
            const string sql = @"
select  nr.NotificationReceiverId, nr.NotificationId, nr.DateRead
        , nr.DateCreated, nr.DateUpdated, nr.DateEnabled, nr.DateDeleted
from    core_NotificationReceiver nr
where   nr.ReceiverId = @UserId
;
select  n.NotificationId, n.NotificationType, n.IconClass, n.Subject, n.Content, n.ReferenceId, n.DateSent
        , n.DateCreated, n.DateUpdated, n.DateEnabled, n.DateDeleted
from    core_Notification n
join    core_NotificationReceiver nr on (nr.NotificationId = n.NotificationId)
where   nr.ReceiverId = @UserId
;
";
            using (var multi = DbConnection.QueryMultiple(sql, new
            {
                query.UserId
            }, DbTransaction))
            {
                var items = multi.Read<NotificationReceiverDto>().AsList();
                var subItems = multi.Read<NotificationDto>().AsList();

                items.ForEach(p =>
                {
                    p.Notification = subItems.SingleOrDefault(q => q.NotificationId == p.NotificationId);
                });

                var count = items.Count;

                var paginated = new PaginatedNotificationReceiverDto(items, query.Page, query.PageSize, count);

                return paginated;
            }
        }

        PaginatedNotificationReceiverDto IQueryHandler<GetUnreadNotificationReceiversByUserIdQuery, PaginatedNotificationReceiverDto>.Handle(GetUnreadNotificationReceiversByUserIdQuery query)
        {
            var today = DateTime.UtcNow;

            const string sql = @"
select  nr.NotificationReceiverId as 'Id', nr.NotificationId, nr.DateRead
        , nr.DateCreated, nr.DateUpdated, nr.DateEnabled, nr.DateDeleted
from    core_NotificationReceiver nr
where   nr.ReceiverId = @UserId
and     nr.DateRead > @Today
;
select  n.NotificationId as 'Id', n.NotificationType, n.Subject, n.Content, n.ReferenceId, n.DateSent
        , n.DateCreated, n.DateUpdated, n.DateEnabled, n.DateDeleted
from    core_Notification n
join    core_NotificationReceiver nr on (nr.NotificationId = n.NotificationId)
where   nr.ReceiverId = @UserId
and     nr.DateRead > @Today
;
";
            using (var multi = DbConnection.QueryMultiple(sql, new
            {
                query.UserId,
                Today = today
            }, DbTransaction))
            {
                var items = multi.Read<NotificationReceiverDto>().AsList();
                var subItems = multi.Read<NotificationDto>().AsList();

                items.ForEach(p =>
                {
                    p.Notification = subItems.SingleOrDefault(q => q.NotificationId == p.NotificationId);
                });

                var count = items.Count;

                var paginated = new PaginatedNotificationReceiverDto(items, query.Page, query.PageSize, count);

                return paginated;
            }
        }


    }
}
