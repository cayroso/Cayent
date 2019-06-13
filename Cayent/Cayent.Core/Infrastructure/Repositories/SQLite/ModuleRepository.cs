using Cayent.Core.Domains.Models.Applications.Modules;
using Cayent.Core.Domains.Models.Applications.Modules.Events;
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
    public sealed class ModuleRepository : BaseRepository<Module>
    {
        public ModuleRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Module Get(string id)
        {
            const string sql = @"
select  m.*
from    core_Module m
where   m.Id = @Id
;
";

            using (var multi = DbConnection.QueryMultiple(sql, new { Id = id }, DbTransaction))
            {
                var data = multi.Read<ModuleData>().SingleOrDefault();

                var model = new Module(data);

                return model;
            }
        }

        public override void Save(Module entity)
        {
            var events = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var @event in events)
            {
                //HandleEvent((dynamic)evt);
                switch (@event)
                {
                    case ModuleCreated evt:
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

        void HandleEvent(ModuleCreated evt)
        {
            const string sql = @"
insert into core_Module(Id, AppId, Title, Description, IconClass, Url, Sequence, DateCreated, DateUpdated, DateEnabled, DateDeleted)
    values  (@Id, @AppId, @Title, @Description, @IconClass, @Url, @Sequence, @DateCreated, @DateUpdated, @DateEnabled, @DateDeleted)
;
";
            DbConnection.Execute(sql, new
            {
                Id = evt.ModuleId.Id,
                AppId = evt.AppId.Id,
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


        #endregion
    }
}
