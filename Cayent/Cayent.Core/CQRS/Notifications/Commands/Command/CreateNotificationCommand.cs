using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Notifications.Commands.Command
{
    public sealed class CreateNotificationCommand : Cayent.CQRS.Commands.Command
    {
        public CreateNotificationCommand(string correlationId, string notificationId, int notificationType, string subject, string content, string referenceId, DateTime dateSent)
            : this(correlationId, notificationId, notificationType, subject, content, referenceId, dateSent,
                  DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {

        }

        public CreateNotificationCommand(string correlationId, string notificationId, int notificationType, string subject, string content, string referenceId, DateTime dateSent,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(correlationId)
        {
            NotificationId = notificationId;
            NotificationType = notificationType;
            Subject = subject;
            Content = content;
            ReferenceId = referenceId;
            DateSent = dateSent;

            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public string NotificationId { get; private set; }
        public int NotificationType { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
        public string ReferenceId { get; private set; }
        public DateTime DateSent { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
