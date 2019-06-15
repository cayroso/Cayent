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
        public AggregateRoot(DateTimeOffset dateCreated, DateTimeOffset dateUpdated, DateTimeOffset dateEnabled, DateTimeOffset dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
        }
    }
}
