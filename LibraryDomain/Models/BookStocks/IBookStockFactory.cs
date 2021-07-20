using LibraryDomain.Models.Books;
using System.Collections.Generic;

namespace LibraryDomain.Models.BookStocks
{
    public interface IBookStockFactory
    {
        List<BookStock> Create(BookId id, int stock);
    }
}
