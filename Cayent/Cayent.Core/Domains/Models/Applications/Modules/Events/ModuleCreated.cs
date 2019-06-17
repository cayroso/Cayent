using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications.Modules.Events
{
    public sealed class ModuleCreated : DomainEvent
    {
        public ModuleCreated(ModuleId moduleId, AppId appId, string title, string description, string iconClass, string url, int sequence,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
        {
            ModuleId = moduleId;
            AppId = appId;
            Title = title;
            Description = description;
            IconClass = iconClass;
            Url = url;
            Sequence = sequence;

            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public ModuleId ModuleId { get; private set; }
        public AppId AppId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string IconClass { get; private set; }
        public string Url { get; private set; }
        public int Sequence { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
