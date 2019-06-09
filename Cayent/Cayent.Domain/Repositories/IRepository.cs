using Cayent.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Get(string id);
        void Save(TEntity entity);
    }
}
