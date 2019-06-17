using Cayent.CQRS.Services;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Cayent.Core.Infrastructure.UnitOfWork.SQLite
{
    public sealed class UnitOfWorkFactory : IDisposable, IUnitOfWorkFactory
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IDbTransaction _dbTransaction;

        public UnitOfWorkFactory(IContainer container, string connectionString)
        {
            _repositoryFactory = container.Resolve<IRepositoryFactory>();

            //_container = container ?? throw new ArgumentNullException(nameof(container));
            //_connectionString = connectionString;

            //_dbConnection = new MySqlConnection(connectionString);
            //_dbConnection.Open();

            //var connection = SqliteFactory.Instance.CreateConnection();
            //connection.ConnectionString = connectionString;
            //connection.StateChange += (s, e) =>
            //{
            //    if (e.CurrentState == ConnectionState.Open)
            //    {
            //        var cmd = connection.CreateCommand();
            //        cmd.CommandText = @"
            //PRAGMA version = 3;
            //PRAGMA DateTimeKind = Utc;
            //PRAGMA synchronous = OFF;
            //PRAGMA journal_mode = WAL;
            //";
            //        cmd.ExecuteScalar();


            //    }
            //};


            //connection.Open();
            //_dbConnection = connection;


            //  SQLITE Version
            var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            //  NOTE: Apply Pragmas in Production
            var pragmas = "PRAGMA version=3; PRAGMA DateTimeKind=Utc; PRAGMA synchronous=OFF; PRAGMA journal_mode=WAL;";
            dbConnection.Execute(pragmas);

            _dbTransaction = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }


        IUnitOfWork IUnitOfWorkFactory.Create()
        {
            var uow = new UnitOfWork(_repositoryFactory, _dbTransaction);

            return uow;
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
                    //_dbConnection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWorkFactory() {
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
