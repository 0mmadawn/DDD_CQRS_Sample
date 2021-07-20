using System;

namespace LibraryDomain.Models.Books
{
    public class BookName : IEquatable<BookName>
    {
        private string Value { get; }

        public BookName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(); }
            if (name.Length > 100) { throw new ArgumentException(); }
            Value = name;
        }

        public override string ToString()
            => Value.ToString();

        public bool Equals(BookName other)
            => this.Value.Equals(other.Value);

        public override bool Equals(object obj)
            => obj is BookName _obj && this.Equals(_obj);

        public override int GetHashCode()
            => this.Value.GetHashCode();

        public static implicit operator string(BookName value)
            => value.Value;

        public static implicit operator BookName(string value)
            => new BookName(value);

        public static bool operator ==(in BookName x, in BookName y)
            => x.Value.Equals(y.Value);
        public static bool operator !=(in BookName x, in BookName y)
            => !x.Value.Equals(y.Value);
    }
}
