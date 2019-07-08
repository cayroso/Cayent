using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
{
    public interface IResponse
    {

    }

    public interface IQuery<out TResponse> where TResponse: IResponse
    {
        /// <summary>
        /// Business transaction id
        /// </summary>
        string CorrelationId { get; set; }
    }

    public abstract class Query<TResponse> : IQuery<TResponse> where TResponse: IResponse
    {
        public Query(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public string CorrelationId { get; set; }
    }
}
