using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.CQRS.Services
{
    public interface IContainer
    {
        T Resolve<T>();
    }
}
