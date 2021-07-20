using System;

namespace LibraryDomain.Models.Users
{
    public class User
    {
        public UserId Id { get; private set; }
        public UserName Name { get; private set; }

        public User(UserId id, UserName name)
        {
            if (id is null) { throw new ArgumentNullException(); }
            if (name is null) { throw new ArgumentNullException(); }
            Id = id;
            Name = name;
        }

        public bool Equals(User other)
            => Equals(this.Id, other.Id);

        public override bool Equals(object obj)
            => obj is User _obj && this.Equals(_obj);

        public override int GetHashCode()
            => HashCode.Combine(Id, Name);

        public static bool operator ==(in User x, in User y)
            => Equals(x, y);
        public static bool operator !=(in User x, in User y)
            => Equals(x, y);
    }
}
