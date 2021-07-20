using LibraryDomain.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace LibraryDomain.Models.BookStocks
{
    public class RentalLimitCountSpecification
    {
        private readonly IBookStockRepository bookStockRepository;

        public RentalLimitCountSpecification(IBookStockRepository bookStockRepository)
        {
            this.bookStockRepository = bookStockRepository;
        }

        // TODO: it's better to define it in the constant class.
        private readonly int MAX_RENTAL_COUNT = 3;

        // each user can borrow up to 3 books
        public bool IsSatisfiedBy(UserId userId)
        {
            IEnumerable<BookStock> stocks = bookStockRepository.FindAll(userId);
            var result = (stocks.Count() < MAX_RENTAL_COUNT);
            return result;
        }
    }
}
