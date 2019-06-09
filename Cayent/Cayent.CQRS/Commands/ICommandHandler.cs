using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void Handle(TCommand command);
    }
}
