using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        void Handle(TEvent @event);
    }
}
