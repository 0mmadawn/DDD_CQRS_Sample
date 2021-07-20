using Dapper;
using LibraryDomain.Models.Books;
using LibraryDomain.Models.BookStocks;
using LibraryDomain.Models.Users;
using LibraryInfrastructure.DataModel;
using LibraryInfrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryInfrastructure.Users
{
    public class BookStockRepository : RepositoryBase, IBookStockRepository
    {
        public void Create(BookStock bookStock)
        {
            var bookStockDataModel = new BookStockDataModel(bookStock);
            this.dbConnection.Execute(
                "INSERT INTO BookStocks(book_id) VALUES (@BookId)",
                new
                {
                    BookId = bookStockDataModel.BookId,
                }
            );
        }

        public IEnumerable<BookStock> FindAll(BookId id)
        {
            // example result of substr Id: 1->'001', 20->'020'
            var queryResult = this.dbConnection.Query<BookStockDataModel>(@"
SELECT
    id AS Id
    ,book_id AS BookId
    ,CASE
        WHEN rental_user_id IS NULL THEN ''
        ELSE substr('000'|| rental_user_id, -3, 3)
    END AS RentalUserId
FROM BookStocks
WHERE
    book_id = @Id
",
                new { Id = id.ToString() }
            );
            var result = queryResult.Select(x => x.ToDomainObject());
            return result;
        }

        public IEnumerable<BookStock> FindAll(UserId userId)
        {
            // example result of substr Id: 1->'001', 20->'020'
            var queryResult = this.dbConnection.Query<BookStockDataModel>(@"
SELECT
    id AS Id
    ,book_id AS BookId
    ,CASE
        WHEN rental_user_id IS NULL THEN ''
        ELSE substr('000'|| rental_user_id, -3, 3)
    END AS RentalUserId
FROM BookStocks
WHERE
    rental_user_id = @UserId
",
                new { UserId = userId.ToString() }
            );
            var result = queryResult.Select(x => x.ToDomainObject());
            return result;
        }

        public void Update(BookStock BookStock)
        {
            var BookStockDataModel = new BookStockDataModel(BookStock);
            this.dbConnection.Execute("UPDATE BookStocks SET rental_user_id = @RentalUserId WHERE id = @Id",
                new
                {
                    RentalUserId = BookStockDataModel.RentalUserId,
                    Id = BookStockDataModel.Id,
                }
            );
        }

        public void Delete(BookStockId BookStockId)
        {
            throw new NotImplementedException();
        }

        public BookStockId NextId()
        {
            // if there is no existing sequence record, latestId value will be set to 0 by QueryFirstOrDefault
            var latestId = this.dbConnection.QueryFirstOrDefault<int>(
                "SELECT seq FROM SQLITE_SEQUENCE WHERE name = 'BookStocks'"
            );

            var result = latestId + 1;
            return result;
        }

    }
}