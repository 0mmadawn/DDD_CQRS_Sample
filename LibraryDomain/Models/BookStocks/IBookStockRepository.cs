using LibraryDomain.Models.Books;
using LibraryDomain.Models.Users;
using System.Collections.Generic;

namespace LibraryDomain.Models.BookStocks
{
    public interface IBookStockRepository
    {
        void Create(BookStock stock);
        IEnumerable<BookStock> FindAll(BookId id);
        IEnumerable<BookStock> FindAll(UserId userId);
        void Update(BookStock stock);
        void Delete(BookStockId bookStockId);
        BookStockId NextId();
    }
}
