using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class DisableAppModuleCommand : Cayent.CQRS.Commands.Command
    {
        public DisableAppModuleCommand(string correlationId, string appId, string moduleId)
            : base(correlationId)
        {
            AppId = appId;
            ModuleId = moduleId;
        }

        public string AppId { get; set; }
        public string ModuleId { get; set; }
    }
}
