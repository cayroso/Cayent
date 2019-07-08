using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, out TResponse> 
        where TQuery : IQuery<TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        TResponse Handle(TQuery query);
    }


}
