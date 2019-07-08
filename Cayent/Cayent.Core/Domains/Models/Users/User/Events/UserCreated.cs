using Cayent.Domain.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Users.User.Events
{
    public sealed class UserCreated : DomainEvent
    {
        public UserCreated(UserId userId, string firstName, string middleName, string lastName, string email, string phone, string mobile,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
        {
            UserId = userId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Mobile = mobile;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public UserId UserId { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Mobile { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public DateTime DateEnabled { get; private set; }
        public DateTime DateDeleted { get; private set; }
    }
}
