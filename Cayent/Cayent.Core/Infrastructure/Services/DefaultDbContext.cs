using Cayent.Domain.Repositories;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Services
{
    public class DefaultDbContext : IDbContext
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public DefaultDbContext(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        IRepository<TEntity> IDbContext.CreateRepository<TEntity>()
        {
            var repo = _repositoryFactory.Create<TEntity>();

            return repo;
        }
    }
}
