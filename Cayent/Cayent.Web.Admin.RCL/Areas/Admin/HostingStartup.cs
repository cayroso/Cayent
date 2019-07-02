using Cayent.Web.Admin.RCL.Areas.Admin;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: HostingStartup(typeof(AdminRCLHostingStartup))]
namespace Cayent.Web.Admin.RCL.Areas.Admin
{
    public class AdminRCLHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
