using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications.Events
{
    public sealed class AppDisabled : DomainEvent
    {
        public AppDisabled(AppId appId)
        {
            AppId = appId;
        }

        public AppId AppId { get; private set; }
    }
}
