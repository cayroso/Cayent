using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cayent.Web.Admin.RCL
{
    internal class AdminRCLConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IHostingEnvironment _environment;
        public AdminRCLConfigureOptions(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public void PostConfigure(string name, StaticFileOptions options)
        {
            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && _environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }
            options.FileProvider = options.FileProvider ?? _environment.WebRootFileProvider;
            // Add our provider
            //var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "resources");
            //options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);

            if (_environment.IsDevelopment())
            {
                // Looks at the physical files on the disk so it can pick up changes to files under wwwroot while the application is running is Visual Studio.
                // The last PhysicalFileProvider enalbles TypeScript debugging but only wants to work with IE. I'm currently unsure how to get TS breakpoints to hit with Chrome.
                options.FileProvider = new CompositeFileProvider(options.FileProvider,
                                                                 new PhysicalFileProvider(Path.Combine(_environment.ContentRootPath, $"..\\{GetType().Assembly.GetName().Name}\\resources")),
                                                                 new PhysicalFileProvider(Path.Combine(_environment.ContentRootPath, $"..\\{GetType().Assembly.GetName().Name}")));
            }
            else
            {
                // When deploying use the files that are embedded in the assembly.
                options.FileProvider = new CompositeFileProvider(options.FileProvider,
                                                                 new ManifestEmbeddedFileProvider(GetType().Assembly, "resources"));
            }

            _environment.WebRootFileProvider = options.FileProvider; // required to make asp-append-version work as it uses the WebRootFileProvider. https://github.com/aspnet/Mvc/issues/7459
        }
    }

    public static class AdminRCLServiceCollectionExtensions
    {
        public static void AddAdminRCL(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(AdminRCLConfigureOptions));
        }
    }
}
