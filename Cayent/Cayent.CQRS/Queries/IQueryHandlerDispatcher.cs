using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
{
    public interface IQueryHandlerDispatcher
    {
        /// <summary>
        /// Handles the query handler for the given query, that returns TResult
        /// </summary>
        /// <typeparam name="TQuery">iquery</typeparam>
        /// <typeparam name="TResult">object</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        TResult Handle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult: IResponse;
    }

    public sealed class QueryHandlerDispatcher : IQueryHandlerDispatcher
    {
        readonly IQueryHandlerFactory _factory;

        public QueryHandlerDispatcher(IQueryHandlerFactory factory)
        {
            _factory = factory;
        }

        public TResult Handle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult: IResponse
        {
            var handler = _factory.Create<TQuery, TResult>();

            var result = handler.Handle(query);

            return result;
        }
    }
}
