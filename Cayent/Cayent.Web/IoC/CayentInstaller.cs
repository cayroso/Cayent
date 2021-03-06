﻿using Castle.Facilities.AspNetCore;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cayent.Core.Domains.Services;
using Cayent.Core.Infrastructure.EventBus.SQLite;
using Cayent.Core.Infrastructure.Repositories.SQLite;
using Cayent.Core.Infrastructure.Services;
using Cayent.Core.Infrastructure.UnitOfWork.SQLite;
using Cayent.CQRS.Commands;
using Cayent.CQRS.Events;
using Cayent.CQRS.Queries;
using Cayent.CQRS.Repositories;
using Cayent.CQRS.Services;
using Cayent.Domain.Repositories;
using Cayent.Infrastructure.EventBus;
using Cayent.Infrastructure.Repositories;
using Cayent.Infrastructure.Services;
using Cayent.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cayent.Web.IoC
{
    public sealed class CayentInstaller : IWindsorInstaller
    {
        private const string NamespacePrefixes = "Cayent.Core, Cayent.CQRS, Cayent.Domain, Cayent.Infrastructure";

        private List<string> _prefixes;
        private List<string> _assemblies;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            _prefixes = NamespacePrefixes.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            //_assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
            //    .Where(p => _prefixes.Any(pref => p.FullName.StartsWith(pref)))
            //    .ToList();

            _assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Where(p => _prefixes.Any(pref => p.FullName.StartsWith(pref)))
                .Select(p => p.FullName)
                .OrderBy(p => p)
                .ToList();

            InstallRepositories(container);
            InstallServices(container);
            InstallApiControllers(container);

        }

        void InstallRepositories(IWindsorContainer container)
        {
            var config = container.Resolve<IConfiguration>();

            //var dbFile = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), config[ ConfigurationManager.AppSettings["db.name"]);
            //var createDb = bool.Parse(ConfigurationManager.AppSettings["db.create"]);
            //var ddlScriptFiles = ConfigurationManager.AppSettings["db.ddlScriptFiles"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //var connectionString = ConfigurationManager.ConnectionStrings["app.db"].ConnectionString;

            var dbFile = "AppDb.sqlite";
            var createDb = true;
            var ddlScriptFiles = @"cayent.ddl.sql".Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //var connectionString = $@"Data Source={dbFile};Version=3;DateTimeKind=Utc;Synchronous=OFF;Journal Mode=WAL;";
            var connectionString = $@"Data Source={dbFile}; Version=3;";


            if (createDb || !File.Exists(dbFile))
            {
                File.Delete(dbFile);

                SQLiteConnection.CreateFile(dbFile);

                using (var con = new SQLiteConnection(connectionString))
                {
                    con.Open();

                    foreach (var ddlScriptFile in ddlScriptFiles)
                    {
                        //  TODO: find another way to find the script files in either bin\debug or bin\release folder
                        var scriptFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ddlScriptFile.Trim());

                        var sql = File.ReadAllText(scriptFile);

                        var cmd = new SQLiteCommand(sql, con);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            //var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).Select(p => p.FullName).ToList();
            container.Register(

                //Component.For<IUnitOfWork>()
                //    .UsingFactoryMethod((k) =>
                //    {
                //        var fac = k.Resolve<IUnitOfWorkFactory>();
                //        var uow = fac.Create();

                //        return uow;
                //    })
                //    .LifestyleTransient()
                //    .CrossWired()
                //    ,
                Component.For<IUnitOfWorkFactory>()
                    .UsingFactoryMethod((k) =>
                    {
                        var repositoryFactory = k.Resolve<IRepositoryFactory>();
                        var fac = new UnitOfWorkFactory(repositoryFactory, connectionString);

                        return fac;
                    })
                    .LifestyleScoped()
                    .CrossWired()
                    ,
                Component.For<IDbContext>()
                    .ImplementedBy<DefaultDbContext>()
                    .LifestyleScoped()
                    .CrossWired()
                    );
            //container.Resolve<IRepository>(new List<KeyValuePair<string, object>>());

            //  register all IRepository<Entity> default implementations
            _assemblies.ForEach(asm =>
            {
                container.Register(

                    Classes.FromAssemblyNamed(asm)
                        .BasedOn(typeof(IRepository<>))

                        //.BasedOn(typeof(BaseRepository<>))
                        .WithService.Base()
                        .LifestyleTransient()


                    );
            });
            
        }

        void InstallServices(IWindsorContainer container)
        {
            //  CQRS Factories and Dispatchers
            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<IWindsorContainer>()
                    .Instance(container)
                    .LifestyleScoped()
                    .CrossWired()
                    );

            container.Register(
                Component.For<IContainer>()
                    .ImplementedBy<DummyContainer>()
                    .LifestyleTransient()
                    );

            container.Register(
                Component.For<IRepositoryFactory>()
                    .ImplementedBy<DefaultRepositoryFactory>()
                    .LifestyleTransient(),

                Component.For<ICommandHandlerFactory>()
                    .ImplementedBy<DefaultCommandHandlerFactory>()
                    .LifestyleTransient(),
                Component.For<ICommandHandlerDispatcher>()
                    .ImplementedBy<CommandHandlerDispatcher>()
                    .LifestyleTransient()
                    .CrossWired(),

                Component.For<IQueryHandlerFactory>()
                    .ImplementedBy<DefaultQueryHandlerFactory>()
                    .LifestyleTransient(),
                Component.For<IQueryHandlerDispatcher>()
                    .ImplementedBy<QueryHandlerDispatcher>()
                    .LifestyleTransient()
                    .CrossWired(),

                Component.For<IEventHandlerFactory>()
                    .ImplementedBy<DefaultEventHandlerFactory>()
                    .LifestyleTransient(),
                Component.For<IEventHandlerDispatcher>()
                    .ImplementedBy<EventHandlerDispatcher>()
                    .LifestyleTransient(),

                Component.For<ISequentialGuidGenerator>()
                    .ImplementedBy<SequentialGuidGenerator>()
                    .LifestyleTransient()
                    .CrossWired(),
                Component.For<IPasswordHasher>()
                    .ImplementedBy<DefaultPasswordHasher>()
                    .LifestyleTransient()
                    .CrossWired(),
                //Component.For<IEventRepository>()
                //    .ImplementedBy<EventRepository>()
                //    .LifestyleTransient(),
                Component.For<IEventBus>()
                    .ImplementedBy<EventBus>()
                    .LifestyleScoped(),
                Component.For<ITransactionManager>()
                    .ImplementedBy<DefaultTransactionManager>()
                    .LifestyleTransient()
                    .CrossWired()
                );


            _assemblies.ForEach(asm =>
            {
                container.Register(
                    Classes.FromAssemblyNamed(asm)
                        .BasedOn(typeof(ICommandHandler<>))
                        .WithService.Base()
                        .LifestyleTransient(),
                    Classes.FromAssemblyNamed(asm)
                        .BasedOn(typeof(IQueryHandler<,>))
                        .WithService.Base()
                        .LifestyleTransient(),
                    Classes.FromAssemblyNamed(asm)
                        .BasedOn(typeof(IEventHandler<>))
                        .WithService.Base()
                        .LifestyleTransient(),
                    Classes.FromAssemblyNamed(asm)
                        .BasedOn<Hub>()
                        //.WithService.Base()
                        .LifestyleTransient()
                        );

                //  register any signalr Hub
                //container.Register(
                //    Component.For<ChatHub>().LifestyleTransient()
                //);
            });
        }

        void InstallApiControllers(IWindsorContainer container)
        {
            _assemblies.ForEach(asm =>
            {
                container.Register(
                    Classes.FromAssemblyNamed(asm)
                        .BasedOn<ControllerBase>()
                        .LifestyleScoped()
                        );
            });
        }

    }

    public class DummyContainer : IContainer
    {
        private readonly IWindsorContainer _windsorContainer;

        public DummyContainer(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer ?? throw new ArgumentNullException(nameof(windsorContainer));
        }

        T IContainer.Resolve<T>()
        {
            var item = _windsorContainer.Resolve<T>();

            return item;
        }

        T IContainer.Resolve<T>(IEnumerable<KeyValuePair<string, object>> arguments)
        {
            var item = _windsorContainer.Resolve<T>(arguments);

            return item;
        }
    }
}
