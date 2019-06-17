using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class DisableAppCommand : Cayent.CQRS.Commands.Command
    {
        public DisableAppCommand(string correlationId, string appId)
            : base(correlationId)
        {
            AppId = appId;
        }

        public string AppId { get; set; }
    }
}
