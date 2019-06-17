using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Domain.Models.Entities
{
    public interface IAggregateRoot
    {
    }

    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateRoot(DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
        }
    }
}
