using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Data
{
    public interface IEntityData
    {
        string Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        DateTime DateEnabled { get; set; }
        DateTime DateDeleted { get; set; }
    }

    public abstract class EntityData : IEntityData
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateEnabled { get; set; }
        public DateTime DateDeleted { get; set; }

        public EntityData()
        {
            Id = string.Empty;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            DateEnabled = DateTime.MaxValue;
            DateDeleted = DateTime.MinValue;
        }
    }
}
