using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Users.User.Events
{
    public sealed class UserEnabled : DomainEvent
    {
        public UserEnabled(UserId userId, DateTime enabledAt)
        {
            UserId = userId;
            EnabledAt = enabledAt;
        }

        public UserId UserId { get; private set; }
        public DateTime EnabledAt { get; private set; }
    }
}
