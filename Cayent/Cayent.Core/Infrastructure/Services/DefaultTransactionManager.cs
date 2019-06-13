using Cayent.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Services
{
    public sealed class DefaultTransactionManager : ITransactionManager
    {
        string ITransactionManager.GetCurrentTransaction()
        {
            //  in web, use httpcontext

            //  others, depend on another service

            return Guid.NewGuid().ToString().ToLower();
        }
    }
}
