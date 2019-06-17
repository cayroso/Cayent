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
                  DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {

        }

        public CreateAppCommand(string correlationId, string appId, string title, string description,
            string iconClass, string url, int sequence,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
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

        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
