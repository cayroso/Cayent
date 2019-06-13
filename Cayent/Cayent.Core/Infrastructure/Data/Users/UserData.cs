using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Users
{
    public class UserData : EntityData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public UserData()
        {
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;

            Email = string.Empty;
            Phone = string.Empty;
            Mobile = string.Empty;
        }
    }
}
