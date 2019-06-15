using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Data
{
    public interface IEntityData
    {
        string Id { get; set; }
        DateTimeOffset DateCreated { get; set; }
        DateTimeOffset DateUpdated { get; set; }
        DateTimeOffset DateEnabled { get; set; }
        DateTimeOffset DateDeleted { get; set; }
    }

    public abstract class EntityData : IEntityData
    {
        public string Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public DateTimeOffset DateEnabled { get; set; }
        public DateTimeOffset DateDeleted { get; set; }

        public EntityData()
        {
            Id = string.Empty;
            DateCreated = DateTimeOffset.Now;
            DateUpdated = DateTimeOffset.Now;
            DateEnabled = DateTimeOffset.MaxValue;
            DateDeleted = DateTimeOffset.MinValue;
        }
    }
}
