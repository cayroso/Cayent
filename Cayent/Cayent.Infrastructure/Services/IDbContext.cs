using Cayent.Domain.Models.Entities;
using Cayent.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Services
{
    public interface IDbContext
    {
        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : Entity;
    }
}
