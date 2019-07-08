using Cayent.Core.Domains.Models.Users.User;
using Cayent.Core.Infrastructure.Data.Notification;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications
{
    public sealed class NotificationReceiver : ValueObject
    {

        public NotificationId NotificationId { get; }
        public UserId ReceiverId { get; }
        public DateTime DateRead { get; private set; }

        public NotificationReceiver(NotificationReceiverData data)
        {

            NotificationId = new NotificationId(data.Id);
            ReceiverId = new UserId(data.ReceiverId);
            DateRead = data.DateRead;
        }

        public NotificationReceiver(NotificationId notificationId, UserId receiverId, DateTime dateRead)
        {
            NotificationId = notificationId;
            ReceiverId = receiverId;
            DateRead = dateRead;
        }

        public void MarkAsRead(DateTime dateRead)
        {
            DateRead = dateRead;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NotificationId;
            yield return ReceiverId;
        }
    }
}
