using Cayent.Domain.Repositories;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Cayent.Core.Infrastructure.UnitOfWork.SQLite
{/// <summary>
/// ////
/// </summary>
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly string UnitOfWorkId = Guid.NewGuid().ToString();
        //private readonly IRepositoryFactory _repositoryFactory;
        private SQLiteTransaction _dbTransaction;

        public UnitOfWork(SQLiteTransaction dbTransaction)
        {
            //_repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));

            _dbTransaction = dbTransaction;
        }

        //IRepository<TEntity> IUnitOfWork.CreateRepository<TEntity>()
        //{
        //    var repo = _repositoryFactory.Create<TEntity>();

        //    return repo;
        //}

        IDbConnection IUnitOfWork.GetDbConnection()
        {
            if (_dbTransaction == null || _dbTransaction.Connection == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            return _dbTransaction.Connection;
        }

        IDbTransaction IUnitOfWork.GetDbTransaction()
        {
            if (_dbTransaction == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            return _dbTransaction;
        }

        void IUnitOfWork.Commit()
        {
            if (_dbTransaction.Connection == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            _dbTransaction.Commit();
            //_dbTransaction.Connection.Close();
            //_dbTransaction.Dispose();
            //_dbConnection = null;
        }

        void IUnitOfWork.Rollback()
        {
            if (_dbTransaction.Connection == null)
            {
                throw new InvalidOperationException("transaction has already been committed/rolledback.");
            }

            _dbTransaction.Rollback();
            //_dbTransaction.Connection.Close();
            //_dbTransaction.Dispose();
            //_dbConnection = null;
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
                    if (_dbTransaction != null && _dbTransaction.Connection != null)
                    {
                        _dbTransaction.Rollback();
                        //_dbTransaction.Dispose();
                        //    _dbTransaction = null;
                    }
                    //_dbTransaction.Dispose();

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
