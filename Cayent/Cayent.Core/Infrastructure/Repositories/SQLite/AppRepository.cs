using Cayent.Core.Domains.Models.Applications;
using Cayent.Core.Domains.Models.Applications.Events;
using Cayent.Core.Infrastructure.Data.Applications;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.Infrastructure.Repositories.SQLite
{
    public sealed class AppRepository : BaseRepository<App>
    {
        public AppRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override App Get(string id)
        {
            const string sql = @"
select  a.Id, a.Title, a.Description, a.IconClass, a.Url, a.Sequence
        , a.DateCreated, a.DateUpdated, a.DateEnabled, a.DateDeleted
from    core_App a
where   a.Id = @Id
;
select  p.Id
from    core_vwPermission p
where   p.AppId = @Id
;
select  m.Id
from    core_Module m
where   m.AppId = @Id
;
";

            using (var multi = DbConnection.QueryMultiple(sql, new { Id = id }, DbTransaction))
            {
                var data = multi.Read<AppData>().SingleOrDefault();

                if (data != null)
                {
                    var permIds = multi.Read<string>().ToList();
                    var moduleIds = multi.Read<string>().ToList();

                    data.PermissionIds = permIds;
                    data.ModuleIds = moduleIds;
                }
                var model = new App(data);

                return model;
            }
        }

        public override void Save(App entity)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var @event in events)
            {
                //HandleEvent((dynamic)evt);
                switch (@event)
                {
                    case AppCreated evt:
                        HandleEvent(evt);
                        break;
                    case AppEnabled evt:
                        HandleEvent(evt);
                        break;
                    case AppDisabled evt:
                        HandleEvent(evt);
                        break;
                    case PermissionAdded evt:
                        HandleEvent(evt);
                        break;
                    case PermissionDisabled evt:
                        HandleEvent(evt);
                        break;
                    case PermissionEnabled evt:
                        HandleEvent(evt);
                        break;
                    case PermissionRemoved evt:
                        HandleEvent(evt);
                        break;

                    case ModuleEnabled evt:
                        HandleEvent(evt);
                        break;
                    case ModuleDisabled evt:
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

        void HandleEvent(AppCreated evt)
        {
            const string sql = @"
insert into core_App(Id, Title, Description, IconClass, Url, Sequence, DateCreated, DateUpdated, DateEnabled, DateDeleted)
    values  (@Id, @Title, @Description, @IconClass, @Url, @Sequence, @DateCreated, @DateUpdated, @DateEnabled, @DateDeleted)
;
";
            DbConnection.Execute(sql, new
            {
                evt.AppId.Id,
                evt.Title,
                evt.Description,
                evt.IconClass,
                evt.Url,
                evt.Sequence,
                evt.DateCreated,
                evt.DateUpdated,
                evt.DateEnabled,
                evt.DateDeleted
            }, DbTransaction);

        }

        void HandleEvent(AppEnabled evt)
        {
            const string sql = @"
update  core_App
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.AppId.Id,
                DateEnabled = DateTimeOffset.MaxValue
            }, DbTransaction);

        }

        void HandleEvent(AppDisabled evt)
        {
            const string sql = @"
update  core_App
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.AppId.Id,
                DateEnabled = evt.OccurredAt
            }, DbTransaction);

        }

        void HandleEvent(PermissionAdded evt)
        {
            const string sql = @"
insert into core_Permission(AppId, PermissionId, DateCreated, DateEnabled)
    values  (@AppId, @PermissionId, @DateCreated, @DateEnabled)
;
";
            DbConnection.Execute(sql, new
            {
                AppId = evt.AppId.Id,
                PermissionId = evt.PermissionId.Id,
                DateCreated = evt.OccurredAt,
                DateEnabled = DateTimeOffset.MaxValue
            }, DbTransaction);

        }

        void HandleEvent(PermissionEnabled evt)
        {
            const string sql = @"
update  core_Permission
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.PermissionId.Id,
                DateEnabled = DateTimeOffset.MaxValue
            }, DbTransaction);

        }

        void HandleEvent(PermissionDisabled evt)
        {
            const string sql = @"
update  core_Permission
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.PermissionId.Id,
                DateEnabled = evt.OccurredAt
            }, DbTransaction);

        }

        void HandleEvent(PermissionRemoved evt)
        {
            const string sql = @"
delete from  core_AxxxpplicationPermission
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                AppId = evt.AppId.Id,
                PermissionId = evt.PermissionId.Id
            }, DbTransaction);

        }

        void HandleEvent(ModuleEnabled evt)
        {
            const string sql = @"
update  core_Module
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.ModuleId.Id,
                DateEnabled = DateTimeOffset.MaxValue
            }, DbTransaction);
        }

        void HandleEvent(ModuleDisabled evt)
        {
            const string sql = @"
update  core_Module
set     DateEnabled = @DateEnabled
where   Id = @Id
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.ModuleId.Id,
                DateEnabled = evt.OccurredAt
            }, DbTransaction);
        }
        #endregion

    }
}
