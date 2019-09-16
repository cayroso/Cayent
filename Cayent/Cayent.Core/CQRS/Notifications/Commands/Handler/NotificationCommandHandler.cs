using Cayent.Core.CQRS.Notifications.Commands.Command;
using Cayent.Core.Domains.Models.Notifications;
using Cayent.CQRS.Commands;
using Cayent.Domain.Repositories;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Notifications.Commands.Handler
{
    public sealed class NotificationCommandHandler :
       ICommandHandler<CreateNotificationCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IRepositoryFactory _repositoryFactory;

        public NotificationCommandHandler(IDbContext dbContext, IRepositoryFactory repositoryFactory)
        {
            _dbContext = dbContext;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        void ICommandHandler<CreateNotificationCommand>.Handle(CreateNotificationCommand command)
        {
            var repo = _dbContext.CreateRepository<Notification>();
            
            var domain = new Notification(new NotificationId(command.NotificationId), command.NotificationType, command.Subject, command.Content, command.ReferenceId, command.DateSent);

            repo.Save(domain);
        }
    }
}
