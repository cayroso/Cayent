using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
