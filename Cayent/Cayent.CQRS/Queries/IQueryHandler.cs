using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        TResponse Handle(TQuery query);
    }
}
