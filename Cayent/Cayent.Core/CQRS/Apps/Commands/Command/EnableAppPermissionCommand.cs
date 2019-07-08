using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class EnableAppPermissionCommand : Cayent.CQRS.Commands.Command
    {
        public EnableAppPermissionCommand(string correlationId,string appId, string permissionId)
            : base(correlationId)
        {
            AppId = AppId;
            PermissionId = permissionId;
        }

        public string AppId { get; set; }
        public string PermissionId { get; set; }
    }
}
