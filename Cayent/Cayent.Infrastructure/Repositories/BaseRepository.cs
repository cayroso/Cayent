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
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        private readonly string RepositoryId = Guid.NewGuid().ToString();
        private IUnitOfWork _unitOfWork;

        protected IDbConnection DbConnection => _unitOfWork.GetDbConnection();
        protected IDbTransaction DbTransaction => _unitOfWork.GetDbTransaction();

        public abstract T Get(string id);
        public abstract void Save(T entity);

        public abstract void ExecuteSql(string sql, object param);
    }
}

