using Cayent.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public abstract class BaseQueryHandler
    {
        protected IDbConnection DbConnection;
        protected IDbTransaction DbTransaction;

        public BaseQueryHandler(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            DbConnection = unitOfWork.GetDbConnection();
            DbTransaction = unitOfWork.GetDbTransaction();
        }
    }
}
