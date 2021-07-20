using LibraryDomain.Models.BookStocks;

namespace LibraryInfrastructure.DataModel
{
    internal class BookStockDataModel
    {
        internal int Id { get; set; }
        internal string BookId { get; set; }
        internal string RentalUserId { get; set; }

        // for Dapper
        internal BookStockDataModel(){}

        internal BookStockDataModel(int id, string bookId, string rentalUserId)
        {
            Id = id;
            BookId = bookId;
            RentalUserId = rentalUserId;
        }

        internal BookStockDataModel(BookStock source)
        {
            Id = source.Id;
            BookId = source.BookId;
            if (source.RentalUserId is not null)
            {
                RentalUserId = source.RentalUserId;
            }
        }

        internal BookStock ToDomainObject()
        {
            if (string.IsNullOrEmpty(RentalUserId))
            {
                return new BookStock(
                    id: Id,
                    bookId: BookId
                );
            }
            else
            {
                return new BookStock(
                    id: Id,
                    bookId: BookId,
                    rentalUserId: RentalUserId
                );
            }
        }
    }
}
