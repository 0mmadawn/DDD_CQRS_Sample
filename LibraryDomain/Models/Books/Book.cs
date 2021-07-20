using System;

namespace LibraryDomain.Models.Books
{
    public class Book : IEquatable<Book>
    {
        public BookId Id { get; private set; }

        public BookName Name { get; private set; }

        public Book(BookId id, BookName name)
        {
            if (id is null) { throw new ArgumentNullException(); }
            if (name is null) { throw new ArgumentNullException(); }
            Id = id;
            Name = name;
        }

        public bool Equals(Book other)
            => Equals(this.Id, other.Id);

        public override bool Equals(object obj)
            => obj is Book _obj && this.Equals(_obj);

        public override int GetHashCode()
            => HashCode.Combine(Id, Name);

        public static bool operator ==(in Book x, in Book y)
            => Equals(x, y);

        public static bool operator !=(in Book x, in Book y)
            => Equals(x, y);
    }
}
