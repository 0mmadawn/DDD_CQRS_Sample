using LibraryDomain.Models.Books;

namespace LibraryDomain.Services
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public bool Exist(BookId id)
        {
            var book = bookRepository.Find(id);
            return book is not null;
        }
    }
}
