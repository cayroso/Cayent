using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications.Events
{
    public sealed class NotificationCreated : DomainEvent
    {
        public NotificationCreated(NotificationId notificationId, int notificationType, string iconClass, string subject, string content,
            string referenceId, DateTime dateSent)
        {
            NotificationId = notificationId;
            NotificationType = notificationType;
            IconClass = iconClass;
            Subject = subject;
            Content = Content;
            ReferenceId = referenceId;
            DateSent = dateSent;
        }

        public NotificationId NotificationId { get;  }
        public int NotificationType { get;}
        public string IconClass { get;  }
        public string Subject { get; }
        public string Content { get; }
        public string ReferenceId { get; }
        public DateTime DateSent { get;}
    }
}
