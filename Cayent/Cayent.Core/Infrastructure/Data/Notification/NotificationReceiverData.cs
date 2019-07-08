using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Notification
{
    public class NotificationReceiverData : EntityData
    {
        public string NotificationId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime DateRead { get; set; }
    }
}
