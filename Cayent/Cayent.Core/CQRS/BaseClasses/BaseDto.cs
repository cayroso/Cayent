using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public abstract class BaseDto
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public DateTimeOffset DateEnabled { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
        public bool IsEnabled { get { return DateEnabled > DateTimeOffset.UtcNow; } }
        public bool IsDeleted { get { return DateDeleted < DateTimeOffset.UtcNow; } }

        public BaseDto()
        {
            DateCreated = DateTimeOffset.MinValue;
            DateUpdated = DateTimeOffset.MinValue;
            DateEnabled = DateTimeOffset.MaxValue;
            DateDeleted = DateTimeOffset.MaxValue;
        }
    }

    public abstract class EntityBaseDto : BaseDto
    {
        public string Id { get; set; }

        public EntityBaseDto()
        {
            Id = string.Empty;
        }
    }
}
