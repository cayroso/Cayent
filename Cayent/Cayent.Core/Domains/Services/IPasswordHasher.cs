using Cayent.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Services
{
    public interface IPasswordHasher : IDomainService
    {
        string Salt();
        string GetSaltedPassword(string password, string salt);
        string Hash(string password, string salt);

        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
