using Cayent.Core.Domains.Models.Applications.Modules.Events;
using Cayent.Core.Infrastructure.Data.Applications;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications.Modules
{
    public sealed class ModuleId : Identity
    {
        public ModuleId(string id) : base(id)
        {
        }
    }

    public sealed class Module : AggregateRoot
    {
        public ModuleId ModuleId { get; private set; }
        public AppId AppId { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string IconClass { get; private set; }
        public string Url { get; private set; }
        public int Sequence { get; private set; }

        public Module(ModuleData data)
            : base(data.DateCreated, data.DateUpdated, data.DateEnabled, data.DateDeleted)
        {
            ModuleId = new ModuleId(data.Id);
            AppId = new AppId(data.AppId);
            Title = data.Title;
            Description = data.Description;
            IconClass = data.IconClass;
            Url = data.Url;
            Sequence = data.Sequence;
        }

        public Module(ModuleId moduleId, AppId appId, string title, string description, string iconClass, string url, int sequence)
            : this(moduleId, appId, title, description, iconClass, url, sequence,
                  DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue)
        {
        }

        public Module(ModuleId moduleId, AppId appId, string title, string description, string iconClass, string url, int sequence,
            DateTimeOffset dateCreated, DateTimeOffset dateUpdated, DateTimeOffset dateEnabled, DateTimeOffset dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
            Apply(new ModuleCreated(moduleId, appId, title, description, iconClass, url, sequence,
                dateCreated, dateUpdated, dateEnabled, dateDeleted));
        }

        void When(ModuleCreated e)
        {
            ModuleId = e.ModuleId;
            AppId = e.AppId;
            Title = e.Title;
            Description = e.Description;
            IconClass = e.IconClass;
            Url = e.Url;
            Sequence = e.Sequence;
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return AppId;
            yield return ModuleId;
        }

        protected override void When(IDomainEvent e)
        {
            When(e as dynamic);
        }
    }
}
