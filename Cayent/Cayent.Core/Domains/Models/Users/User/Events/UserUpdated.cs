using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Users.User.Events
{
    public sealed class UserUpdated : DomainEvent
    {
        public UserUpdated(UserId userId, string firstName, string middleName, string lastName, string email, string phone, string mobile, DateTime updatedAt)
        {
            UserId = userId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            UpdatedAt = updatedAt;
        }

        public UserId UserId { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Mobile { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
