using Cayent.Core.Domains.Models.Applications.Modules;
using Cayent.Core.Domains.Models.Applications.Modules.Events;
using Cayent.Core.Domains.Models.Notifications;
using Cayent.Core.Domains.Models.Notifications.Events;
using Cayent.Core.Infrastructure.Data.Applications;
using Cayent.Core.Infrastructure.Data.Notification;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.Infrastructure.Repositories.SQLite
{
    public sealed class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {

        }

        public override Notification Get(string id)
        {
            const string sql = @"
select  *
from    core_Notification
where   NotificationId = @Id
;
--  receivers
select  *
from    core_NotificationReceiver nr
where   nr.NotificationId = @Id
";

            using (var multi = DbConnection.QueryMultiple(sql, new { Id = id }, DbTransaction))
            {
                var data = multi.Read<NotificationData>().SingleOrDefault();

                if (data != null)
                {
                    var receivers = multi.Read<NotificationReceiverData>().AsList();
                    data.Receivers = receivers;
                }

                var model = new Notification(data);

                
                return model;
            }
        }

        public override void Save(Notification entity)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var @event in events)
            {
                //HandleEvent((dynamic)evt);
                switch (@event)
                {
                    case NotificationCreated evt:
                        HandleEvent(evt);
                        break;

                }
            }
        }

        public override void ExecuteSql(string sql, object param)
        {
            DbConnection.Execute(sql, param, DbTransaction);
        }

        #region Domain Event Handlers

        void HandleEvent(NotificationCreated evt)
        {
            const string sql = @"
insert into core_Notification(NotificationId, NotificationType, Subject, Content, Referenceid, DateSent, DateCreated, DateUpdated, DateEnabled, DateDeleted)
    values  (@Id, @NotificationType, @Subject, @Content, @Referenceid, @DateSent, @DateCreated, @DateUpdated, @DateEnabled, @DateDeleted)
;
";
            DbConnection.Execute(sql, new
            {
                evt.NotificationId.Id,
                evt.NotificationType,
                evt.Subject,
                evt.Content,
                evt.ReferenceId,
                evt.DateSent,

                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                DateEnabled = DateTime.MaxValue,
                DateDeleted = DateTime.MaxValue
            }, DbTransaction);

        }


        #endregion
    }
}
