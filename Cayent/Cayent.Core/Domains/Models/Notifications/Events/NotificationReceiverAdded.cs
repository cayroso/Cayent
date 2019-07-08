using Cayent.Core.Domains.Models.Users.User;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications.Events
{
    public sealed class NotificationReceiverAdded : DomainEvent
    {
        public NotificationReceiverAdded(NotificationId notificationId, UserId receiverId)
        {
            NotificationId = notificationId;
            ReceiverId = receiverId;
        }

        public NotificationId NotificationId { get;  }
        public UserId ReceiverId { get;}
    }
}
