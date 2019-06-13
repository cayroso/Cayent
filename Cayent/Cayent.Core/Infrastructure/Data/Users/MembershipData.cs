using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Users
{
    public class MembershipData : EntityData
    {
        public List<string> RoleIds { get; set; }


        public MembershipData()
        {
            RoleIds = new List<string>();
        }
    }

    public class MembershipRoleData : RelationData
    {
        public MembershipRoleData()
        {
            MembershipId = string.Empty;
            RoleId = string.Empty;
        }

        public string MembershipId { get; set; }
        public string RoleId { get; set; }
    }

    public class MembershipAppData : RelationData
    {
        public MembershipAppData()
        {
            MembershipId = string.Empty;
            AppId = string.Empty;
        }

        public string MembershipId { get; set; }
        public string AppId { get; set; }
    }

    public class MembershipModuleData : RelationData
    {
        public MembershipModuleData()
        {
            MembershipId = string.Empty;
            ModuleId = string.Empty;
        }
        public string MembershipId { get; set; }
        public string ModuleId { get; set; }
    }
}
