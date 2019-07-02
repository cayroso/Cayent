using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Cayent.Web.Admin.RCL;
using Cayent.Web.Admin.RCL.Areas.Admin;
using Cayent.Web.Admin.RCL.Areas.Admin.ViewComponents;
using Cayent.Web.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cayent.Web.App
{
    public class Startup
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.Configure<CookiePolicyOptions>(options =>
        //    {
        //        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //        options.CheckConsentNeeded = context => true;
        //        options.MinimumSameSitePolicy = SameSiteMode.None;
        //    });


        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //}
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Setup component model contributors for making windsor services available to IServiceProvider
            Container.AddFacility<AspNetCoreFacility>(f => f.CrossWiresInto(services));

            services.AddAdminRCL();

            // Add framework services.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();
            services.AddLogging((lb) => lb.AddConsole().AddDebug());
            //services.AddSingleton<FrameworkMiddleware>(); // Do this if you don't care about using Windsor to inject dependencies

            // Custom application component registrations, ordering is important here
            RegisterApplicationComponents(services);

            // Castle Windsor integration, controllers, tag helpers and view components, this should always come after RegisterApplicationComponents
            var foo = services.AddWindsor(Container,
                opts =>
                {
                    //opts.UseEntryAssembly(typeof(Startup).Assembly);
                    //opts.UseEntryAssembly(typeof(AdminRCLHostingStartup).Assembly);
                    //opts.UseEntryAssembly(typeof(AdminNavbarViewComponent).Assembly);

                }, // <- Recommended
                () => services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = false })); // <- Optional

            return foo;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Container.GetFacility<AspNetCoreFacility>().RegistersMiddlewareInto(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            Container.Register(Component.For<IConfiguration>().Instance(Configuration));

            //Container.AddFacility<FactorySupportFacility>();

            //  install from cayent
            Container.Install(FromAssembly.Containing<CayentInstaller>());

            //  install from app
            //Container.Install(FromAssembly.Containing<CastleInstaller>());
        }
    }
}
