using Cayent.CQRS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Commands
{
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Creates a command handler for the command
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;
    }

    public sealed class DefaultCommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IContainer _container;

        public DefaultCommandHandlerFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        ICommandHandler<TCommand> ICommandHandlerFactory.Create<TCommand>()
        {
            var handler = _container.Resolve<ICommandHandler<TCommand>>();

            return handler;
        }
    }
}
