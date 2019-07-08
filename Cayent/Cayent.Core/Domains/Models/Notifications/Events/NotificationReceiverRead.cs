using Cayent.Core.Domains.Models.Users.User;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications.Events
{
    public sealed class NotificationReceiverRead : DomainEvent
    {
        public NotificationReceiverRead(NotificationId notificationId, UserId receiverId, DateTime dateRead)
        {
            NotificationId = notificationId;
            ReceiverId = receiverId;
            DateRead = dateRead;
        }

        public NotificationId NotificationId { get;  }
        public UserId ReceiverId { get;}
        public DateTime DateRead { get; }
    }
}
