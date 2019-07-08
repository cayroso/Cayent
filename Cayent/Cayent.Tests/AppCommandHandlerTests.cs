using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Cayent.Core.CQRS.Apps.Commands.Command;
using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.CQRS.Commands;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Cayent.Web.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Cayent.Tests
{
    [TestClass]
    public class AppCommandHandlerTests
    {
        private static WindsorContainer Container;
        IDisposable scope;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            // Executes once for the test class. (Optional)
            Container = new WindsorContainer();

            var configurationRoot = new ConfigurationBuilder().Build();

            Container.Register(Component.For<IConfiguration>().Instance(configurationRoot));

            // register your dependencies
            Container.Install(FromAssembly.Containing<CayentInstaller>());
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
            Container.Dispose();
        }

        [TestInitialize]
        public void SetUp()
        {
            //Container = new WindsorContainer();

            //var configurationRoot = new ConfigurationBuilder().Build();

            //Container.Register(Component.For<IConfiguration>().Instance(configurationRoot));

            //// register your dependencies
            //Container.Install(FromAssembly.Containing<CayentInstaller>());

            scope = Container.BeginScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Container.Dispose();
            scope.Dispose();
            
        }

        [TestMethod]
        public void AppCreated()
        {
            //  ARRANGE
            var uow = Container.Resolve<IUnitOfWork>();
            var cmdDispatcher = Container.Resolve<ICommandHandlerDispatcher>();

            for (var i = 0; i < 10; i++)
            {
                var cmd = new CreateAppCommand("x" + i.ToString(), "x" + i.ToString(), "x" + i.ToString(), "x", "x", "x", 1);

                //  ACT
                cmdDispatcher.Handle(cmd);
            }

            //  ASSERT

            uow.Commit();
        }

        [TestMethod]
        public void AppDisabled()
        {
            //  ARRANGE
            var uow = Container.Resolve<IUnitOfWork>();
            var cmdDispatcher = Container.Resolve<ICommandHandlerDispatcher>();
            var queryDispatcher = Container.Resolve<IQueryHandlerDispatcher>();

            var cmd = new CreateAppCommand("AppDisabled", "AppDisabled", "AppDisabled", "x", "x", "x", 1);
            cmdDispatcher.Handle(cmd);

            var query = new GetAppByIdQuery("AppDisabled", cmd.AppId);
            var dto = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query);
            
            //  ACT
            Thread.Sleep(2000);
            
            var cmd2 = new DisableAppCommand("AppDisabled", cmd.AppId);
            cmdDispatcher.Handle(cmd2);
            
            var query2 = new GetAppByIdQuery("AppDisabled", cmd2.AppId);
            var dto2 = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query2);

            //  ASSERT
            Assert.IsTrue(dto.IsEnabled);
            Assert.IsFalse(dto2.IsEnabled);

            uow.Commit();
        }

        [TestMethod]
        public void AppEnabled()
        {
            //  ARRANGE
            var uow = Container.Resolve<IUnitOfWork>();
            var cmdDispatcher = Container.Resolve<ICommandHandlerDispatcher>();
            var queryDispatcher = Container.Resolve<IQueryHandlerDispatcher>();

            var cmd = new CreateAppCommand("EnableAppCommand", "AppEnabled", "AppEnabled", "x", "x", "x", 1);
            cmdDispatcher.Handle(cmd);

            var query = new GetAppByIdQuery("EnableAppCommand", cmd.AppId);
            var dto = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query);

            //  ACT
            Thread.Sleep(200);

            var cmd2 = new DisableAppCommand("EnableAppCommand", cmd.AppId);
            cmdDispatcher.Handle(cmd2);

            Thread.Sleep(200);

            var cmd3 = new EnableAppCommand("EnableAppCommand", cmd.AppId);
            cmdDispatcher.Handle(cmd3);
            
            //  ASSERT
            var query2 = new GetAppByIdQuery("EnableAppCommand", cmd2.AppId);
            var dto2 = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query2);

            Assert.IsTrue(dto.IsEnabled);

            uow.Commit();
        }


        [TestMethod]
        public void ModuleCreated()
        {
            //  ARRANGE
            var uow = Container.Resolve<IUnitOfWork>();
            var cmdDispatcher = Container.Resolve<ICommandHandlerDispatcher>();

            var cmd = new CreateAppCommand("CreateModule", "CreateModule", "x", "x", "x", "x", 1);
            var cmd2 = new CreateModuleCommand("CreateModule", cmd.AppId, "CreateModule", "CreateModule", "CreateModule", "CreateModule", "CreateModule", 1);

            //  ACT
            cmdDispatcher.Handle(cmd);
            cmdDispatcher.Handle(cmd2);


            //  ASSERT
            uow.Commit();
        }



        [TestMethod]
        public void ModuleDisabled()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ModuleEnabled()
        {
            throw new NotImplementedException();

        }

        [TestMethod]
        public void PermissionAdded()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PermissionDisabled()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PermissionEnabled()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PermissionRemoved()
        {
            throw new NotImplementedException();
        }
    }
}
