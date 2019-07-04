using Cayent.Core.CQRS.Apps.Commands.Command;
using Cayent.Core.Domains.Models.Applications;
using Cayent.Core.Domains.Models.Applications.Modules;
using Cayent.Core.Domains.Models.Permissions;
using Cayent.CQRS.Commands;
using Cayent.Domain.Repositories;
using Cayent.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Commands.Handler
{
    public sealed class AppCommandHandler :
        ICommandHandler<CreateAppCommand>,
        ICommandHandler<EnableAppCommand>,
        ICommandHandler<DisableAppCommand>,
        ICommandHandler<CreateModuleCommand>,
        ICommandHandler<EnableAppModuleCommand>,
        ICommandHandler<DisableAppModuleCommand>,
        ICommandHandler<AddAppPermissionCommand>,
        ICommandHandler<EnableAppPermissionCommand>,
        ICommandHandler<DisableAppPermissionCommand>,
        ICommandHandler<RemoveAppPermissionCommand>


    {
        readonly IRepositoryFactory _repositoryFactory;
        readonly IRepository<App> _repoApplication;
        readonly IRepository<Module> _repoModule;

        public AppCommandHandler(IRepositoryFactory repositoryFactory, IRepository<App> repoApplication, IRepository<Module> repoModule)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _repoApplication = repoApplication ?? throw new ArgumentNullException(nameof(repoApplication));
            _repoModule = repoModule ?? throw new ArgumentNullException(nameof(repoModule));
        }

        void ICommandHandler<CreateAppCommand>.Handle(CreateAppCommand command)
        {
            var domain = new App(new AppId(command.AppId),
                command.Title, command.Description, command.IconClass, command.Url, command.Sequence,
                command.DateCreated, command.DateUpdated, command.DateEnabled, command.DateDeleted);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<EnableAppCommand>.Handle(EnableAppCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.Enable();

            _repoApplication.Save(domain);
        }

        void ICommandHandler<DisableAppCommand>.Handle(DisableAppCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.Disable();

            _repoApplication.Save(domain);
        }

        void ICommandHandler<CreateModuleCommand>.Handle(CreateModuleCommand command)
        {
            var domain = new Module(new ModuleId(command.ModuleId), new AppId(command.AppId),
                command.Title, command.Description, command.IconClass, command.Url, command.Sequence,
                command.DateCreated, command.DateUpdated, command.DateEnabled, command.DateDeleted);

            _repoModule.Save(domain);
        }

        void ICommandHandler<AddAppPermissionCommand>.Handle(AddAppPermissionCommand command)
        {
            var permRepo = _repositoryFactory.Create<Permission>();
            var domain = _repoApplication.Get(command.AppId);

            var perm = new Permission(new PermissionId(command.PermissionId), domain.AppId, command.Name, command.Description);

            permRepo.Save(perm);


            domain.AddPermission(command.PermissionId);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<EnableAppPermissionCommand>.Handle(EnableAppPermissionCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.EnablePermission(command.PermissionId);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<DisableAppPermissionCommand>.Handle(DisableAppPermissionCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.DisablePermission(command.PermissionId);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<RemoveAppPermissionCommand>.Handle(RemoveAppPermissionCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.RemovePermission(command.PermissionId);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<EnableAppModuleCommand>.Handle(EnableAppModuleCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.EnableModule(command.ModuleId);

            _repoApplication.Save(domain);
        }

        void ICommandHandler<DisableAppModuleCommand>.Handle(DisableAppModuleCommand command)
        {
            var domain = _repoApplication.Get(command.AppId);

            domain.DisableModule(command.ModuleId);

            _repoApplication.Save(domain);
        }
    }
}
