using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Notification
{
    public class NotificationData : EntityData
    {
        public NotificationData()
        {
            Id = string.Empty;
            NotificationType = 0;
            IconClass = string.Empty;
            Subject = string.Empty;
            Content = string.Empty;
            ReferenceId = string.Empty;
            DateSent = DateTime.MaxValue;
        }

        public int NotificationType { get; set; }
        public string IconClass { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DateSent { get; set; }
    }
}
