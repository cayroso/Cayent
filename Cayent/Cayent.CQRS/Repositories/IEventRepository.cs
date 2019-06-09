using Cayent.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Repositories
{
    public interface IEventRepository
    {
        void Add(IEvent @event);
        void IncrementRetryCount(string eventId);
        void UpdateDateSuccess(string eventId);
        void UpdateDateFailure(string eventId);
        void UpdateTransactionDateFailure(string correlationId);

        TEvent GetEventOfType<TEvent>() where TEvent : class, IEvent;
        TEvent GetEventOfType<TEvent>(string correlationId) where TEvent : class, IEvent;
        //TEvent GetEventOfTypeByTenant<TEvent>(string tenantId) where TEvent : class, IEvent;
        IEnumerable<IEvent> GetUnproccesedEvents();
        IEnumerable<IEvent> GetEvents(string correlationId);
        IEnumerable<IEvent> GetFailedEvents();
    }
}
