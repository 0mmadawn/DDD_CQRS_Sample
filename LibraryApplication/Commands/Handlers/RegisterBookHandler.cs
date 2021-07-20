using LibraryApplication.Commands.Requests;
using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using System.Collections.Generic;
using System.Transactions;

namespace LibraryApplication.Commands.Handlers
{
    public class RegisterBookHandler
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookStockRepository bookStockRepository;
        private readonly IBookStockFactory bookStockFactory;
        
        public RegisterBookHandler(
            IBookRepository bookRepository,
            IBookStockRepository bookStockRepository,
            IBookStockFactory bookStockFactory)
        {
            this.bookRepository = bookRepository;
            this.bookStockRepository = bookStockRepository;
            this.bookStockFactory = bookStockFactory;
        }

        public void Handle(RegisterBookCommand request)
        {
            Book book = new Book(request.Id, request.Name);
            using (var transaction = new TransactionScope())
            {
                List<BookStock> bookStocks = bookStockFactory.Create(request.Id, request.Stock);
                bookRepository.Create(book);
                bookStocks.ForEach(bookStock => bookStockRepository.Create(bookStock));
                transaction.Complete();
            }
        }
    }
}
