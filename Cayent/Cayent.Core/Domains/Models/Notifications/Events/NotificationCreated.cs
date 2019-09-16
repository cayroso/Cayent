using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications.Events
{
    public sealed class NotificationCreated : DomainEvent
    {
        public NotificationCreated(NotificationId notificationId, int notificationType, string subject, string content,
            string referenceId, DateTime dateSent)
        {
            NotificationId = notificationId;
            NotificationType = notificationType;
            Subject = subject;
            Content = content;
            ReferenceId = referenceId;
            DateSent = dateSent;
        }

        public NotificationId NotificationId { get;  }
        public int NotificationType { get;}
        public string Subject { get; }
        public string Content { get; }
        public string ReferenceId { get; }
        public DateTime DateSent { get;}
    }
}
