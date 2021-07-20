using System;

namespace LibraryDomain.Models.Books
{
    public class BookStockId : IEquatable<BookStockId>
    {
        private int Value { get; }

        public BookStockId(int id)
        {
            Value = id;
        }
        public bool Equals(BookStockId other)
            => this.Value.Equals(other.Value);
        public override bool Equals(object obj)
            => obj is BookStockId _obj && this.Equals(_obj);

        public override int GetHashCode()
            => this.Value.GetHashCode();

        public static implicit operator int(BookStockId value)
            => value.Value;

        public static implicit operator BookStockId(int value)
            => new BookStockId(value);

        public static bool operator ==(in BookStockId x, in BookStockId y)
            => x.Value.Equals(y.Value);
        public static bool operator !=(in BookStockId x, in BookStockId y)
            => !x.Value.Equals(y.Value);
    }
}
