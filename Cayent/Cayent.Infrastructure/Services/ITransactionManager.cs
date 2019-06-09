using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Services
{
    public interface ITransactionManager
    {
        /// <summary>
        /// Creates a new or get the current transaction
        /// </summary>
        /// <returns></returns>
        string GetCurrentTransaction();
    }
}
