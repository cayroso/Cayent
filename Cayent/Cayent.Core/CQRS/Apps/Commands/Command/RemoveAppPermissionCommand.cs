using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class RemoveAppPermissionCommand : Cayent.CQRS.Commands.Command
    {
        public RemoveAppPermissionCommand(string correlationId,string appId, string permissionId)
            : base(correlationId)
        {
            AppId = AppId;
            PermissionId = permissionId;
        }

        public string AppId { get; set; }
        public string PermissionId { get; set; }
    }
}
