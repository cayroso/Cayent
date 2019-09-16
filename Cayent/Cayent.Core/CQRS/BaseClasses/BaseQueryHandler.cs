using Cayent.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public abstract class BaseQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IDbConnection DbConnection => _unitOfWork.GetDbConnection();
        protected IDbTransaction DbTransaction => _unitOfWork.GetDbTransaction();

        public BaseQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));

            }
            _unitOfWork = unitOfWorkFactory.Create();
        }
    }
}
