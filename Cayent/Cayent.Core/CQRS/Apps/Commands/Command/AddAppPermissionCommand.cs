using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class AddAppPermissionCommand : Cayent.CQRS.Commands.Command
    {
        public AddAppPermissionCommand(string correlationId, string appId, string permissionId, string name, string description)
            : this(correlationId, appId, permissionId, name, description,
                  DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {

        }

        public AddAppPermissionCommand(string correlationId, string appId, string permissionId, string name, string description,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(correlationId)
        {
            AppId = appId;
            PermissionId = permissionId;
            Name = name;
            Description = description;
            
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }
        
        public string AppId { get; private set; }
        public string PermissionId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
