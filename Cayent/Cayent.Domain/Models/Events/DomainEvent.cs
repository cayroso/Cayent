using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Domain.Models.Events
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredAt { get; set; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        public DomainEvent()
        {
            OccurredAt = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset OccurredAt { get; set; }
    }
}
