using Cayent.Domain.Models.Entities;
using Cayent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbConnection GetDbConnection();
        IDbTransaction GetDbTransaction();
        void Commit();
        void Rollback();

        //IRepository<TEntity> CreateRepository<TEntity>() where TEntity : Entity;
    }
}
