using Cayent.Core.Domains.Models.Applications;
using Cayent.Core.Domains.Models.Permissions.Events;
using Cayent.Core.Infrastructure.Data.Permissions;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Permissions
{
    public sealed class PermissionId : Identity
    {
        public PermissionId(string id) : base(id)
        {
        }
    }

    public sealed class Permission : AggregateRoot
    {
        public PermissionId PermissionId { get; private set; }
        public AppId AppId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Permission(PermissionData data)
            : base(data.DateCreated, data.DateUpdated, data.DateEnabled, data.DateDeleted)
        {
            PermissionId = new PermissionId(data.Id);
            AppId = new AppId(data.AppId);
            Name = data.Name;
            Description = data.Description;
        }

        public Permission(PermissionId permissionId, AppId appId, string name, string description)
            : this(permissionId, appId, name, description,
                 DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {


        }

        public Permission(PermissionId permissionId, AppId appId, string name, string description,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
            Apply(new PermissionCreated(permissionId, appId, name, description, dateCreated, dateUpdated, dateEnabled, dateDeleted));
        }

        void When(PermissionCreated e)
        {
            PermissionId = e.PermissionId;
            AppId = e.AppId;
            Name = e.Name;
            Description = e.Description;

        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return PermissionId;
        }

        protected override void When(IDomainEvent e)
        {
            When(e as dynamic);
        }
    }
}
