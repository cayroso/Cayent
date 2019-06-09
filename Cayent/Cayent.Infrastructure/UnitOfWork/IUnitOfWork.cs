using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection GetDbConnection();
        IDbTransaction GetDbTransaction();
        void Commit();
        void Rollback();
    }
}
