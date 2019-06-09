using Cayent.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Publish(IEvent @event);
    }
}
