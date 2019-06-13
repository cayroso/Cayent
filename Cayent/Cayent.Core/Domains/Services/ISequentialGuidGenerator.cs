using Cayent.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Services
{
    public interface ISequentialGuidGenerator : IDomainService
    {
        string GuidString();
    }
}
