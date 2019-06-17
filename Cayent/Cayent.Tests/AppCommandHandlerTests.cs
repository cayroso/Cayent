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

        //IDisposable scope;

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

            //scope = Container.BeginScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Container.Dispose();
            //scope.Dispose();
            
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
            //uow2.Rollback();
            //disp.Dispose();
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
        public void AppDisabled()
        {
            //  ARRANGE
            var scope = Container.BeginScope();
            var uow1 = Container.Resolve<IUnitOfWork>();
            var cmdDispatcher = Container.Resolve<ICommandHandlerDispatcher>();
            var queryDispatcher = Container.Resolve<IQueryHandlerDispatcher>();

            var cmd = new CreateAppCommand("AppDisabled", "AppDisabled", "AppDisabled", "x", "x", "x", 1);
            cmdDispatcher.Handle(cmd);

            var query = new GetAppByIdQuery("AppDisabled", cmd.AppId);
            var dto = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query);

            uow1.Commit();
            scope.Dispose();

            //  ACT
            Thread.Sleep(1500);
            var scope2 = Container.BeginScope();
            var uow2 = Container.Resolve<IUnitOfWork>();

            var cmdDispatcher2 = Container.Resolve<ICommandHandlerDispatcher>();
            var queryDispatcher2 = Container.Resolve<IQueryHandlerDispatcher>();

            var cmd2 = new DisableAppCommand("AppDisabled", cmd.AppId);
            cmdDispatcher2.Handle(cmd2);
            
            var query2 = new GetAppByIdQuery("AppDisabled", cmd2.AppId);
            var dto2 = queryDispatcher.Handle<GetAppByIdQuery, AppDto>(query2);

            //  ASSERT

            Assert.IsFalse(dto2.IsEnabled);

            uow2.Commit();
            scope2.Dispose();
        }

        [TestMethod]
        public void AppEnabled()
        {

        }

        [TestMethod]
        public void ModuleDisabled()
        {

        }

        [TestMethod]
        public void ModuleEnabled()
        {

        }

        [TestMethod]
        public void PermissionAdded()
        {

        }

        [TestMethod]
        public void PermissionDisabled()
        {

        }

        [TestMethod]
        public void PermissionEnabled()
        {

        }

        [TestMethod]
        public void PermissionRemoved()
        {

        }
    }
}
