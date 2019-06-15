using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Data
{
    public interface IRelationData
    {
        DateTimeOffset DateCreated { get; set; }
        DateTimeOffset DateEnabled { get; set; }
    }

    public abstract class RelationData
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateEnabled { get; set; }

        public RelationData()
        {
            DateCreated = DateTimeOffset.Now;
            DateEnabled = DateTimeOffset.MaxValue;
        }
    }
}
