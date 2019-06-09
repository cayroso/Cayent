using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Events
{
    public interface IEventHandlerDispatcher
    {
        /// <summary>
        /// Calls the Handle of the event handler for the given event
        /// </summary>
        /// <typeparam name="TEvent">subclass of IEvent</typeparam>
        /// <param name="@event">event to send</param>
        void Handle<TEvent>(TEvent @event) where TEvent : IEvent;
    }

    public sealed class EventHandlerDispatcher : IEventHandlerDispatcher
    {
        readonly IEventHandlerFactory _factory;

        public EventHandlerDispatcher(IEventHandlerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException("factory");
        }

        public void Handle<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handler = _factory.Create<TEvent>();

            handler.Handle(@event);
        }
    }
}
