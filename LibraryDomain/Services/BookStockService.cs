using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using LibraryDomain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDomain.Services
{
    public class BookStockService
    {
        private readonly IBookStockRepository bookStockRepository;

        public BookStockService(IBookStockRepository bookStockRepository)
        {
            this.bookStockRepository = bookStockRepository;
        }

        public bool IsAvailable(BookId bookId)
        {
            IEnumerable<BookStock> bookStocks = bookStockRepository.FindAll(bookId);
            var result = bookStocks.Any(x => x.IsAvailable());
            return result;
        }

        public BookStock GetAvailable(BookId bookId)
        {
            IEnumerable<BookStock> bookStocks = bookStockRepository.FindAll(bookId);
            var stock = bookStocks.FirstOrDefault(x => x.IsAvailable());
            if (stock is null) { throw new Exception(); }
            return stock;
        }

        // TODO: doubtful method
        public bool Lend(ref BookStock availableStock, UserId userId)
        {
            if (availableStock is null) { throw new ArgumentNullException(); }
            if (userId is null) { throw new ArgumentNullException(); }
            if (!availableStock.IsAvailable()) { throw new InvalidOperationException(); }

            var rentalLimitCountSpecification
                = new RentalLimitCountSpecification(bookStockRepository);
            if (!rentalLimitCountSpecification.IsSatisfiedBy(userId))
            {
                return false;
            }

            availableStock.ChangeRentalUserId(userId);
            return true;
        }
    }
}
