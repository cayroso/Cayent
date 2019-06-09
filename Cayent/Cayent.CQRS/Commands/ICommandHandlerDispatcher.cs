using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Commands
{
    public interface ICommandHandlerDispatcher
    {

        /// <summary>
        /// Calls the handle method of the command handler for the given command
        /// </summary>
        /// <typeparam name="TCommand">subclass of ICommand</typeparam>
        /// <param name="command">command to execute</param>
        void Handle<TCommand>(TCommand command) where TCommand : ICommand;

        //void Handle<TCommand>(TCommand[] commands) where TCommand : ICommand;
    }

    public sealed class CommandHandlerDispatcher : ICommandHandlerDispatcher
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandHandlerDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException("factory");
        }

        //public void Handle<TCommand>(TCommand command) where TCommand : ICommand
        //{
        //    var handler = _factory.Create<TCommand>();

        //    handler.Handle(command);
        //}

        void ICommandHandlerDispatcher.Handle<TCommand>(TCommand command)
        {
            var handler = _factory.Create<TCommand>();
            handler.Handle(command);
        }

    }
}
