using Cayent.Domain.Repositories;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Core.Infrastructure.UnitOfWork.SQLite
{
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly string UnitOfWorkId = Guid.NewGuid().ToString();
        private readonly IRepositoryFactory _repositoryFactory;
        private IDbTransaction _dbTransaction;

        public UnitOfWork(IRepositoryFactory repositoryFactory, IDbTransaction dbTransaction)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            
            _dbTransaction = dbTransaction;
        }

        IRepository<TEntity> IUnitOfWork.CreateRepository<TEntity>()
        {
            var repo = _repositoryFactory.Create<TEntity>();

            return repo;
        }

        IDbConnection IUnitOfWork.GetDbConnection()
        {
            return _dbTransaction.Connection;
        }
        IDbTransaction IUnitOfWork.GetDbTransaction()
        {
            return _dbTransaction;
        }

        void IUnitOfWork.Commit()
        {
            if (_dbTransaction == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            _dbTransaction.Commit();
            _dbTransaction = null;
        }

        void IUnitOfWork.Rollback()
        {
            if (_dbTransaction == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            _dbTransaction.Rollback();
            _dbTransaction = null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (_dbTransaction != null)
                    {
                        _dbTransaction.Rollback();
                        _dbTransaction = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }




        #endregion
    }
}
