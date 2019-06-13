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
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
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

        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
