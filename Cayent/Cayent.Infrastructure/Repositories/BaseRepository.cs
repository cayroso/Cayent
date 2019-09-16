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
        public BaseRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            //_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork = unitOfWorkFactory.Create();
        }
        private readonly string RepositoryId = Guid.NewGuid().ToString();
        private readonly IUnitOfWork _unitOfWork;

        protected IDbConnection DbConnection => _unitOfWork.GetDbConnection();
        protected IDbTransaction DbTransaction => _unitOfWork.GetDbTransaction();

        public abstract T Get(string id);
        public abstract void Save(T entity);

        public abstract void ExecuteSql(string sql, object param);
    }
}

