using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Domain.Models.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredAt { get; set; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            OccurredAt = DateTime.UtcNow;
        }

        public DateTime OccurredAt { get; set; }
    }
}
