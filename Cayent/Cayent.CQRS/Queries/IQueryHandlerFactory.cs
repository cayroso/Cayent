using Cayent.CQRS.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
{
    public interface IQueryHandlerFactory
    {
        /// <summary>
        /// Creates a query handler for the given query and result
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IQueryHandler<TQuery, TResult> Create<TQuery, TResult>() where TQuery : IQuery<TResult> where TResult: IResponse;
    }

    public sealed class DefaultQueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IContainer _container;

        public DefaultQueryHandlerFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        IQueryHandler<TQuery, TResult> IQueryHandlerFactory.Create<TQuery, TResult>()
        {
            var handler = _container.Resolve<IQueryHandler<TQuery, TResult>>();

            return handler;
        }
    }
}
