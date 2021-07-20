namespace LibraryDomain.Models.Books
{
    public interface IBookRepository
    {
        void Create(Book book);
        Book Find(BookId id);
        Book Find(BookName name);
        void Update(Book book);
        void Delete(BookId bookId);
    }
}
