using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Applications
{
    public class ModuleData : EntityData
    {
        public ModuleData()
        {
            AppId = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            IconClass = string.Empty;
            Url = string.Empty;
            Sequence = 99999;
        }

        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public int Sequence { get; set; }
    }
}
