using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
{
    public interface IQuery<out TResponse>
    {
        /// <summary>
        /// Business transaction id
        /// </summary>
        string CorrelationId { get; set; }
    }

    public abstract class Query<TResponse> : IQuery<TResponse>
    {
        public Query(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public string CorrelationId { get; set; }
    }
}
