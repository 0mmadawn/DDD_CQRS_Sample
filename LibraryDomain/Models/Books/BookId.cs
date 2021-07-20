using System;
using System.Text.RegularExpressions;

namespace LibraryDomain.Models.Books
{
    /// like ISBN-13
    public class BookId : IEquatable<BookId>
    {
        private string Value { get; }

        public BookId(string id)
        {
            if (string.IsNullOrEmpty(id)) { throw new ArgumentNullException(); }
            // only contain number or hyphen
            if (!Regex.IsMatch(id, @"\A[0-9\-]+\z")) { throw new ArgumentException(); }
            // length must be 13 except hyphen
            if (id.Replace("-", "").Length != 13) { throw new ArgumentException(); }
            Value = id;
        }

        public override string ToString()
            =>Value.ToString();

        public bool Equals(BookId other)
            => this.Value.Equals(other.Value);
        public override bool Equals(object obj)
            => obj is BookId _obj && this.Equals(_obj);

        public override int GetHashCode()
            => this.Value.GetHashCode();

        public static implicit operator string(BookId value)
            => value.Value;

        public static implicit operator BookId(string value)
            => new BookId(value);

        public static bool operator ==(in BookId x, in BookId y)
            => x.Value.Equals(y.Value);
        public static bool operator !=(in BookId x, in BookId y)
            => !x.Value.Equals(y.Value);
    }
}
