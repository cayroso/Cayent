using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Permissions
{
    public class PermissionData : EntityData
    {
        public string AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionData()
        {
            AppId = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
