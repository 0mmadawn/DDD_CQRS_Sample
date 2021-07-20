using System;
using System.Text.RegularExpressions;

namespace LibraryDomain.Models.Users
{
    // ex. 001, 002, 003, ...
    public class UserId : IEquatable<UserId>
    {
        private string Value { get; }

        public UserId(string id)
        {
            if (string.IsNullOrEmpty(id)) { throw new ArgumentNullException(); }
            if (!Regex.IsMatch(id, @"\A[0-9]{3}\z")) { throw new ArgumentException(); }
            Value = id;
        }

        public int ToInt()
            => int.Parse(this.Value);

        public bool Equals(UserId other)
            => this.Value.Equals(other.Value);

        public override bool Equals(object obj)
            => obj is UserId _obj && this.Equals(_obj);

        public override int GetHashCode()
            => this.Value.GetHashCode();

        public static implicit operator string(UserId value)
            => value.Value;

        public static implicit operator UserId(string value)
            => new UserId(value);

        public static bool operator ==(in UserId x, in UserId y)
            => x.Value.Equals(y.Value);
        public static bool operator !=(in UserId x, in UserId y)
            => !x.Value.Equals(y.Value);
    }
}
