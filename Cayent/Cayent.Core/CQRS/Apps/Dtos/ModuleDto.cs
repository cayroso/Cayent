using Cayent.Core.CQRS.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Dtos
{
    public class ModuleDto : BaseDto
    {
        public ModuleDto()
        {
            AppId = string.Empty;
            ModuleId = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            IconClass = string.Empty;
            Url = string.Empty;
        }

        public string AppId { get; set; }
        public string ModuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }

    }
}
