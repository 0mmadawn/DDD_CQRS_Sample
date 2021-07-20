using LibraryDomain.Models.Books;
using LibraryDomain.Models.Users;
using System;

namespace LibraryDomain.Models.BookStocks
{
    public class BookStock : IEquatable<BookStock>
    {
        public BookStockId Id { get; private set; }

        public BookId BookId { get; private set; }

        public UserId RentalUserId { get; private set; }

        public BookStock(BookStockId id, BookId bookId, UserId rentalUserId = null)
        {
            if (id is null) { throw new ArgumentNullException(); }
            if (bookId is null) { throw new ArgumentNullException(); }
            Id = id;
            BookId = bookId;
            if (rentalUserId is not null)
            {
                RentalUserId = rentalUserId;
            }
        }

        public void ChangeRentalUserId(UserId userId)
        {
            if (userId is null) { throw new ArgumentNullException(); }
            if (RentalUserId is not null) { throw new InvalidOperationException(); }
            RentalUserId = userId;
        }

        public bool IsAvailable()
            => RentalUserId is null;

        public bool Equals(BookStock other)
            => Equals(this.Id, other.Id) 
                && Equals(this.BookId, other.BookId);

        public override bool Equals(object obj)
            => obj is BookStock _obj && this.Equals(_obj);

        public override int GetHashCode()
            => HashCode.Combine(Id, BookId);

        public static bool operator ==(in BookStock x, in BookStock y)
            => Equals(x, y);
        public static bool operator !=(in BookStock x, in BookStock y)
            => Equals(x, y);
    }
}
