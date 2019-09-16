using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cayent.Domain.Models.Entities
{
    public interface IEntity
    {

    }

    public abstract class Entity : IEntity
    {
        public Entity(DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
        {
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            DateEnabled = dateEnabled;
            DateDeleted = dateDeleted;
        }

        public int Version { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateUpdated { get; protected set; }
        public DateTime DateEnabled { get; protected set; }
        public DateTime DateDeleted { get; protected set; }

        readonly IList<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => new ReadOnlyCollection<IDomainEvent>(this.domainEvents);

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }

        //protected abstract void When<T>(IDomainEvent e);// where T : class, IDomainEvent;
        //{
        //(this as dynamic).When(e);
        //   ((dynamic)this).When((dynamic)e);
        //}
        protected abstract void When<T>(T e) where T: class, IDomainEvent;

        protected void Apply(IDomainEvent e)
        {
            domainEvents.Add(e);
            When(e);
            //((dynamic)this).When((dynamic)e);
        }

        /// <summary>
        /// When overriden in a derived class, gets all components of the identity of the entity.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetIdentityComponents();

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (object.ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            var other = obj as Entity;
            return GetIdentityComponents().SequenceEqual(other.GetIdentityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetIdentityComponents());
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
