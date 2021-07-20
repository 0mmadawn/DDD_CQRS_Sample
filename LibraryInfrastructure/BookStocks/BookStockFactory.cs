using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using System.Collections.Generic;
using System.Linq;

namespace LibraryInfrastructure.Books
{
    public class BookStockFactory : IBookStockFactory
    {
        private readonly IBookStockRepository bookStockRepository;

        public BookStockFactory(IBookStockRepository bookStockRepository)
        {
            this.bookStockRepository = bookStockRepository;
        }

        public List<BookStock> Create(BookId id, int stock)
        {
            var nextBookStockId = bookStockRepository.NextId();

            List<BookStock> stocks =
                Enumerable
                    .Range(nextBookStockId, stock)
                    .Select(bookStockId => new BookStock(bookStockId, id))
                    .ToList();
            return stocks;

        }
    }
}
