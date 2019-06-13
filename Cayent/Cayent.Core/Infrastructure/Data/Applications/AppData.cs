using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Applications
{
    public class AppData : EntityData
    {
        public AppData()
        {
            Title = string.Empty;
            Description = string.Empty;
            IconClass = string.Empty;
            Url = string.Empty;
            Sequence = 99999;
            PermissionIds = new List<string>();
            ModuleIds = new List<string>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public int Sequence { get; set; }
        public List<string> PermissionIds { get; set; }
        public List<string> ModuleIds { get; set; }
    }
}
