using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Events
{
    public interface IEvent
    {
        string Id { get; set; }
        string CorrelationId { get; set; }
        DateTimeOffset OccurredAt { get; set; }
    }

    public abstract class Event : IEvent
    {
        public string CorrelationId { get; set; }
        public string Id { get; set; }
        public DateTimeOffset OccurredAt { get; set; }

        public Event(string correlationId, string id, DateTimeOffset occuredAt)
        {
            Id = id;
            CorrelationId = correlationId;
            OccurredAt = occuredAt;
        }
    }
}
