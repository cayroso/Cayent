using Cayent.Core.Domains.Models.Users.User.Events;
using Cayent.Core.Infrastructure.Data.Users;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Domains.Models.Users.User
{
    public sealed class UserId : Identity
    {
        public UserId(string id) : base(id)
        {
        }
    }

    public class User : AggregateRoot
    {
        public UserId Id { get; protected set; }

        public string FirstName { get; protected set; }
        public string MiddleName { get; protected set; }
        public string LastName { get; protected set; }

        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public string Mobile { get; protected set; }

        public User(UserData data)
            : base(data.DateCreated, data.DateUpdated, data.DateEnabled, data.DateDeleted)
        {
            Id = new UserId(data.Id);
            FirstName = data.FirstName;
            MiddleName = data.MiddleName;
            LastName = data.LastName;
            Email = data.Email;
            Phone = data.Phone;
            Mobile = data.Mobile;
        }

        public User(UserId id, string firstName, string middleName, string lastName, string email, string phone, string mobile)
            : this(id, firstName, middleName, lastName, email, phone, mobile,
                 DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {
        }

        public User(UserId id, string firstName, string middleName, string lastName, string email, string phone, string mobile,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnable, DateTime dateDeleted)
            : base(dateCreated, dateUpdated, dateEnable, dateDeleted)
        {
            Apply(new UserCreated(id, firstName, middleName, lastName, email, phone, mobile,
                dateCreated, dateUpdated, dateEnable, dateDeleted));
        }

        void When(UserCreated e)
        {
            Id = e.UserId;
            FirstName = e.FirstName;
            MiddleName = e.MiddleName;
            LastName = e.LastName;
            Email = e.Email;
            Phone = e.Phone;
            Mobile = e.Mobile;

        }

        public void Update(string firstName, string middleName, string lastName, string email, string phone, string mobile)
        {
            Apply(new UserUpdated(Id, firstName, middleName, lastName, email, phone, mobile, DateTime.UtcNow));
        }
        void When(UserUpdated e)
        {
            FirstName = e.FirstName;
            MiddleName = e.MiddleName;
            LastName = e.LastName;

            Email = e.Email;
            Phone = e.Phone;
            Mobile = e.Mobile;

            DateUpdated = e.UpdatedAt;
        }

        public void Enable()
        {
            Apply(new UserEnabled(Id, DateTime.MaxValue));
        }
        void When(UserEnabled e)
        {
            DateEnabled = e.EnabledAt;
        }

        public void Disable()
        {
            Apply(new UserDisabled(Id, DateTime.UtcNow));
        }
        void When(UserDisabled e)
        {
            DateEnabled = e.DisabledAt;
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }
        protected override void When<T>(T e)
        {
            When(e as dynamic);
        }
        //protected override void When(IDomainEvent e)
        //{
        //    When(e as dynamic);
        //}
    }
}
