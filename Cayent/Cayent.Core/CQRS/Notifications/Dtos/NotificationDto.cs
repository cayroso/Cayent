using Cayent.Core.CQRS.BaseClasses;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Notifications.Dtos
{
    public class NotificationDto : EntityBaseDto, IResponse
    {
        public int NotificationType { get; set; }
        public string IconClass { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DateSent { get; set; }
    }
}
