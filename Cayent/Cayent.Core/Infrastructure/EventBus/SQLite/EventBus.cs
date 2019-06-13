using Cayent.CQRS.Events;
using Cayent.CQRS.Repositories;
using Cayent.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.EventBus.SQLite
{
    public sealed class EventBus : IEventBus
    {
        private readonly IEventRepository _eventRepository;

        public EventBus(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException("eventRepository");
        }

        void IEventBus.Publish(IEvent @event)
        {
            _eventRepository.Add(@event);
        }
    }
}
