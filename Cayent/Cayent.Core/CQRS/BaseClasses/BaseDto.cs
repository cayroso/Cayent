using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public abstract class BaseDto
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateEnabled { get; set; }
        public DateTime DateDeleted { get; set; }
        public bool IsEnabled { get { return DateEnabled > DateTime.UtcNow; } }
        public bool IsDeleted { get { return DateDeleted < DateTime.UtcNow; } }

        public BaseDto()
        {
            DateCreated = DateTime.MinValue;
            DateUpdated = DateTime.MinValue;
            DateEnabled = DateTime.MaxValue;
            DateDeleted = DateTime.MaxValue;
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
