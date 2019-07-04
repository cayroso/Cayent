using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class DisableAppPermissionCommand : Cayent.CQRS.Commands.Command
    {
        public DisableAppPermissionCommand(string correlationId, string appId, string permissionId)
            : base(correlationId)
        {
            AppId = appId;
            PermissionId = permissionId;
        }

        public string AppId { get; set; }
        public string PermissionId { get; set; }
    }
}
