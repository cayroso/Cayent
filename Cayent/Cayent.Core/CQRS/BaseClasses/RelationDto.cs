using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public class RelationDto
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateEnabled { get; set; }
        public bool IsEnabled { get { return DateEnabled > DateTimeOffset.UtcNow; } }

        public RelationDto()
        {
            DateCreated = DateTimeOffset.Now;
            DateEnabled = DateTimeOffset.MaxValue;
        }
    }
}
