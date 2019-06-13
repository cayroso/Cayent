using Cayent.Core.Domains.Models.Applications.Modules;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications.Events
{
    public sealed class ModuleEnabled : DomainEvent
    {
        public ModuleEnabled(AppId appId, ModuleId moduleId)
        {
            AppId = appId;
            ModuleId = moduleId;
        }
        public AppId AppId { get; private set; }
        public ModuleId ModuleId { get; private set; }
    }
}
