using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Roles
{
    public class RoleData : EntityData
    {
        public RoleData()
        {
            Name = string.Empty;
            Description = string.Empty;
            PermissionIds = new List<string>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> PermissionIds { get; set; }

    }

    public class RolePermissionData : RelationData
    {
        public RolePermissionData()
        {
            RoleId = string.Empty;
            PermissionId = string.Empty;
        }

        public string RoleId { get; set; }
        public string PermissionId { get; set; }


    }
}
