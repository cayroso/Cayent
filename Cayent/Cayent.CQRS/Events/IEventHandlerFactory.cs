using Cayent.CQRS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Events
{
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        IEventHandler<TEvent> Create<TEvent>() where TEvent : IEvent;
    }

    public sealed class DefaultEventHandlerFactory : IEventHandlerFactory
    {
        private readonly IContainer _container;

        public DefaultEventHandlerFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        IEventHandler<TEvent> IEventHandlerFactory.Create<TEvent>()
        {
            var handler = _container.Resolve<IEventHandler<TEvent>>();

            return handler;
        }
    }
}
