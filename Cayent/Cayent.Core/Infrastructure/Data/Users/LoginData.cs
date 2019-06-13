using Cayent.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Users
{
    public class LoginData : EntityData
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }

        public LoginData()
        {
            Username = string.Empty;
            HashedPassword = string.Empty;
            Salt = string.Empty;
        }
    }
}
