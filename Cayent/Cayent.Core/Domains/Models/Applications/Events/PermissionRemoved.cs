using Cayent.Core.Domains.Models.Permissions;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications.Events
{
    public sealed class PermissionRemoved : DomainEvent
    {
        public PermissionRemoved(AppId appId, PermissionId permissionId)
        {
            AppId = appId;
            PermissionId = permissionId;
        }

        public AppId AppId { get; private set; }
        public PermissionId PermissionId { get; private set; }
    }
}
