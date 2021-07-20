using LibraryApplication.Commands.Requests;
using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using LibraryDomain.Models.Users;
using LibraryDomain.Services;
using System.Transactions;

namespace LibraryApplication.Commands.Handlers
{
    public class LendBookHandler
    {
        private readonly IBookStockRepository bookStockRepository;
        private readonly BookService bookService;
        private readonly BookStockService bookStockService;

        public LendBookHandler(
            IBookStockRepository bookStockRepository,
            BookService bookService,
            BookStockService bookStockService)
        {
            this.bookStockRepository = bookStockRepository;
            this.bookService = bookService;
            this.bookStockService = bookStockService;
        }

        public (bool success, string error) Handle(LendBookCommand request)
        {
            var bookId = new BookId(request.BookId);
            using (var transaction = new TransactionScope())
            {
                // TODO: this should return an error code rather than a string
                if (!bookService.Exist(bookId))
                {
                    return (false, "the book you specified does not exist");
                }
                if (!bookStockService.IsAvailable(bookId))
                {
                    return (false, "all stocks are being lent out");
                }
                var availableStock = bookStockService.GetAvailable(bookId);

                if (!bookStockService.Lend(ref availableStock, new UserId(request.UserId)))
                {
                    return (false, "rental books count is max");
                }
                bookStockRepository.Update(availableStock);
                transaction.Complete();
            }
            return (true, string.Empty);
        }
    }
}
