using Cayent.Core.Domains.Models.Notifications.Events;
using Cayent.Core.Domains.Models.Users.User;
using Cayent.Core.Infrastructure.Data.Notification;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.Domains.Models.Notifications
{
    public sealed class NotificationId : Identity
    {
        public NotificationId(string id) : base(id)
        {
        }
    }

    public sealed class Notification : AggregateRoot
    {
        public NotificationId NotificationId { get; private set; }
        public int NotificationType { get; private set; }
        public string IconClass { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }
        public string ReferenceId { get; private set; }
        public DateTime DateSent { get; private set; }

        public List<NotificationReceiver> Receivers { get; }

        public Notification(NotificationData data)
            : base(data.DateCreated, data.DateUpdated, data.DateEnabled, data.DateDeleted)
        {

            NotificationId = new NotificationId(data.Id);
            NotificationType = data.NotificationType;
            IconClass = data.IconClass;
            Subject = data.Subject;
            Content = data.Content;
            ReferenceId = data.ReferenceId;
            DateSent = data.DateSent;
        }

        public Notification(NotificationId notificationId, int notificationType, string iconClass, string subject, string content,
            string referenceId, DateTime dateSent)
            : this(notificationId, notificationType, iconClass, subject, content, referenceId, dateSent,
                  DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {

        }

        public Notification(NotificationId notificationId, int notificationType, string iconClass, string subject, string content,
            string referenceId, DateTime dateSent,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
            Apply(new NotificationCreated(notificationId, notificationType, iconClass, subject, content,
                referenceId, dateSent));
        }

        public void AddReceiver(UserId receiverId)
        {
            Apply(new NotificationReceiverAdded(NotificationId, receiverId));
        }

        public void RemoveReceiver(UserId receiverId)
        {
            var item = Receivers.SingleOrDefault(p => p.NotificationId == NotificationId && p.ReceiverId == receiverId);

            if (item != null)
            {
                Apply(new NotificationReceiverRemoved(NotificationId, receiverId));
            }
        }

        public void ReadNotification(UserId receiverId, DateTime dateRead)
        {
            var item = Receivers.SingleOrDefault(p => p.NotificationId == NotificationId && p.ReceiverId == receiverId);

            if (item != null)
            {
                Apply(new NotificationReceiverRead(NotificationId, receiverId, dateRead));
            }
        }

        void When(NotificationCreated e)
        {
            NotificationId = e.NotificationId;
            NotificationType = e.NotificationType;
            IconClass = e.IconClass;
            Subject = e.Subject;
            Content = e.Content;
            ReferenceId = e.ReferenceId;
            DateSent = e.DateSent;
            DateCreated = DateTime.UtcNow;

        }

        void When(NotificationReceiverAdded e)
        {
            var valueObject = new NotificationReceiver(e.NotificationId, e.ReceiverId, DateTime.MaxValue);

            Receivers.Add(valueObject);
        }

        void When(NotificationReceiverRemoved e)
        {
            var item = Receivers.Single(p => p.NotificationId == e.NotificationId && p.ReceiverId == p.ReceiverId);

            Receivers.Remove(item);
        }

        void When(NotificationReceiverRead e)
        {
            var item = Receivers.Single(p => p.NotificationId == e.NotificationId && p.ReceiverId == p.ReceiverId);

            item.MarkAsRead(e.DateRead);
        }


        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return NotificationId;
        }

        protected override void When(IDomainEvent e)
        {
            When(e as dynamic);
        }
    }
}
