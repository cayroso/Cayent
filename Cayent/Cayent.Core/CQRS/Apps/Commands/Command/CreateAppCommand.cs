using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Command
{
    public sealed class CreateAppCommand : Cayent.CQRS.Commands.Command
    {
        public CreateAppCommand(string correlationId, string appId, string title, string description, 
            string iconClass, string url, int sequence)
            : this(correlationId, appId, title, description, iconClass, url, sequence,
                  DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue)
        {

        }

        public CreateAppCommand(string correlationId, string appId, string title, string description,
            string iconClass, string url, int sequence,
            DateTimeOffset dateCreated, DateTimeOffset dateUpdated, DateTimeOffset dateEnabled, DateTimeOffset dateDeleted)
            : base(correlationId)
        {
            AppId = appId;
            Title = title;
            Description = description;
            IconClass = iconClass;
            Url = url;
            Sequence = sequence;

            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public string AppId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string IconClass { get; private set; }
        public string Url { get; private set; }
        public int Sequence { get; private set; }

        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset DateUpdated { get; private set; }
        public DateTimeOffset DateEnabled { get; private set; }
        public DateTimeOffset DateDeleted { get; private set; }
    }
}
