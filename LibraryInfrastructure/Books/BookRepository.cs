using Dapper;
using LibraryDomain.Models.Books;
using LibraryInfrastructure.DataModel;
using LibraryInfrastructure.Shared;
using System;

namespace LibraryInfrastructure.Users
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public void Create(Book book)
        {
            var bookDataModel = new BookDataModel(book);
            this.dbConnection.Execute(
                "INSERT INTO Books(Id, Name) VALUES (@Id, @Name)",
                new
                {
                    Id = bookDataModel.Id,
                    Name = bookDataModel.Name
                }
            );
        }

        public Book Find(BookId id)
        {
            var queryResult = this.dbConnection.QueryFirstOrDefault<BookDataModel>(@"
SELECT
    id AS Id
    ,name AS Name
FROM Books
WHERE
    id = @Id
",
                new { Id = id.ToString() }
            );
            Book result = queryResult.ToDomainObject();
            return result;
        }

        public Book Find(BookName name)
        {
            var queryResult = this.dbConnection.QueryFirstOrDefault<BookDataModel>(@"
SELECT
    id AS Id
    ,name AS Name
FROM Books
WHERE
    name = @Name
",
                new { Name = name.ToString() }
            );
            Book result = queryResult.ToDomainObject();
            return result;
        }

        public void Update(Book book)
        {
            var bookDataModel = new BookDataModel(book);
            this.dbConnection.Execute(
                "UPDATE Books(Name) VALUES(@Name) WHERE id = @Id",
                new
                {
                    Name = bookDataModel.Name,
                    Id = bookDataModel.Id,
                }
            );
        }

        public void Delete(BookId bookId)
        {
            throw new NotImplementedException();
        }

    }
}
