using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Business transaction id
        /// </summary>
        string CorrelationId { get; set; }
    }

    public abstract class Command : ICommand
    {
        public Command(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public string CorrelationId { get; set; }
    }
}
