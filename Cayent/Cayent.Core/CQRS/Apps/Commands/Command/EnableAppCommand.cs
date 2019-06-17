using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class EnableAppCommand : Cayent.CQRS.Commands.Command
    {
        public EnableAppCommand(string correlationId, string appId)
            : base(correlationId)
        {
            AppId = appId;
        }

        public string AppId { get; set; }
    }
}
