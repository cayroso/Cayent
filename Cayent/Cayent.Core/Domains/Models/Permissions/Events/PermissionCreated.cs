using Cayent.Core.Domains.Models.Applications;
using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Permissions.Events
{
    public sealed class PermissionCreated : DomainEvent
    {
        public PermissionCreated(PermissionId permissionId, AppId appId, string name, string description,
            DateTimeOffset dateCreated, DateTimeOffset dateUpdated, DateTimeOffset dateEnabled, DateTimeOffset dateDeleted)
        {
            PermissionId = permissionId;
            AppId = appId;
            Name = name;
            Description = description;

            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public PermissionId PermissionId { get; private set; }
        public AppId AppId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset DateUpdated { get; private set; }
        public DateTimeOffset DateEnabled { get; private set; }
        public DateTimeOffset DateDeleted { get; private set; }
    }
}
