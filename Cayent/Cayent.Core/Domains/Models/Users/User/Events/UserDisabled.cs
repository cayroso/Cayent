using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Users.User.Events
{
    public sealed class UserDisabled : DomainEvent
    {
        public UserDisabled(UserId userId, DateTime disabledAt)
        {
            UserId = userId;
            DisabledAt = disabledAt;
        }

        public UserId UserId { get; private set; }
        public DateTime DisabledAt { get; private set; }
    }
}
