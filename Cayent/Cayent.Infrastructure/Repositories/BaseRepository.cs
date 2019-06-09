using Cayent.Domain.Models.Entities;
using Cayent.Domain.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly IDbConnection DbConnection;
        protected readonly IDbTransaction DbTransaction;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            DbConnection = unitOfWork.GetDbConnection();
            DbTransaction = unitOfWork.GetDbTransaction();
        }

        public abstract T Get(string id);
        public abstract void Save(T entity);

        public abstract void ExecuteSql(string sql, object param);
    }
}

