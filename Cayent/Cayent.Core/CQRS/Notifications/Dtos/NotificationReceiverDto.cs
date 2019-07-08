using Cayent.Core.CQRS.BaseClasses;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Notifications.Dtos
{
    public class NotificationReceiverDto : EntityBaseDto, IResponse
    {
        public string NotificationId { get; set; }
        public NotificationDto Notification { get; set; }
        public DateTime DateRead { get; set; }

        public bool IsRead => DateRead <= DateTime.UtcNow;
    }

    public class PaginatedNotificationReceiverDto : Paginated<NotificationReceiverDto>, IResponse
    {
        public PaginatedNotificationReceiverDto(List<NotificationReceiverDto> list, int page, int pageSize, int itemCount)
            : base(list, page, pageSize, itemCount)
        {
        }
    }
}
