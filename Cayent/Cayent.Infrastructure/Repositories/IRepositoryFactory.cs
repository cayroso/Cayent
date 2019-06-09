using Cayent.CQRS.Services;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>() where TEntity : Entity;
    }

    sealed class DefaultRepositoryFactory : IRepositoryFactory
    {
        private readonly IContainer _container;

        public DefaultRepositoryFactory(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        IRepository<TEntity> IRepositoryFactory.Create<TEntity>()
        {
            var handler = _container.Resolve<IRepository<TEntity>>();

            return handler;
        }
    }
}
