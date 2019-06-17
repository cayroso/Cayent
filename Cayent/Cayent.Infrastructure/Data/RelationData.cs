using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Data
{
    public interface IRelationData
    {
        DateTime DateCreated { get; set; }
        DateTime DateEnabled { get; set; }
    }

    public abstract class RelationData
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateEnabled { get; set; }

        public RelationData()
        {
            DateCreated = DateTime.Now;
            DateEnabled = DateTime.MaxValue;
        }
    }
}
