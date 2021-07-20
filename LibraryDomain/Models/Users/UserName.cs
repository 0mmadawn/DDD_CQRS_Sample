using System;

namespace LibraryDomain.Models.Users
{
    public class UserName : IEquatable<UserName>
    {
        public string FirstName { get; }

        public string FamilyName { get; }

        public UserName(string firstName, string familyName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(familyName)) 
            {
                throw new ArgumentNullException();
            }
            if (firstName?.Length > 30
                || familyName?.Length > 30)
            {
                throw new ArgumentException();
            }

            FirstName = firstName;
            FamilyName = familyName;
        }

        public bool Equals(UserName other)
            => this.FirstName.Equals(other.FirstName)
                && this.FamilyName.Equals(other.FamilyName);

        public override bool Equals(object obj)
            => obj is UserName _obj && this.Equals(_obj);

        public override int GetHashCode()
            => HashCode.Combine(FirstName, FamilyName);

        public static bool operator ==(in UserName x, in UserName y)
            => Equals(x, y);
        public static bool operator !=(in UserName x, in UserName y)
            => !Equals(x, y);
    }
}
