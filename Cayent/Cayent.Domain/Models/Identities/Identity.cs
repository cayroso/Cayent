using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Domain.Models.Identities
{
    public interface IIdentity
    {
        string Id { get; }
    }

    public class Identity : IEquatable<Identity>, IIdentity
    {

        public Identity() : this(Guid.NewGuid().ToString())
        {
        }

        public Identity(string id)
        {
            Id = id;
            Version = 0;
        }

        public string Id { get; protected set; }
        public int Version { get; protected set; }

        public bool Equals(Identity id)
        {
            if (ReferenceEquals(this, id)) return true;
            if (ReferenceEquals(null, id)) return false;
            return Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public static bool operator ==(Identity left, Identity right)
        {
            return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
        }

        public static bool operator !=(Identity left, Identity right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

    }
}
