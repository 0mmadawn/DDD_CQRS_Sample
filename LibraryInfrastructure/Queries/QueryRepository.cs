using Dapper;
using LibraryApplication.Queries.RepositoryIf;
using LibraryApplication.Queries.Requets;
using LibraryApplication.Queries.Results;
using LibraryApplication.Users;
using LibraryInfrastructure.Shared;
using System.Collections.Generic;

namespace LibraryInfrastructure.Queries
{
    public class QueryRepository : RepositoryBase, IQueryRepository
    {
        public IEnumerable<GetAllBooksResult> GetAllBooks()
        {
            var result = this.dbConnection.Query<GetAllBooksResult>(@"
SELECT
    B.id AS Id
    ,B.name AS Name
    ,IsBookAVailable.IsAvailable AS IsAvailable
FROM Books B
    LEFT OUTER JOIN (
        SELECT
            BS.book_id
            ,CASE
                WHEN COUNT(*) > 0 THEN True -- True
                ELSE False                  -- FALSE
            END AS IsAvailable
        FROM BookStocks BS
        WHERE 
            BS.rental_user_id IS NULL
        GROUP BY BS.book_id
    ) AS IsBookAVailable
        ON IsBookAVailable.book_id = B.id
ORDER BY B.id;
");
            return result;
        }

        public IEnumerable<GetAllUsersResult> GetAllUser()
        {
            // example result of substr Id: 1->'001', 20->'020'
            var result = this.dbConnection.Query<GetAllUsersResult>(@"
SELECT
    substr('000'|| id, -3, 3) AS Id
    ,first_name AS FirstName
    ,family_name AS FamilyName
FROM 
    Users
ORDER BY id
");
            return result;
        }

        public GetUserResult GetUserById(GetUserQuery request)
        {
            // example result of substr Id: 1->'001', 20->'020'
            var result = this.dbConnection.QueryFirstOrDefault<GetUserResult>(@"
SELECT
    substr('000'|| id, -3, 3) AS Id
    ,first_name AS FirstName
    ,family_name AS FamilyName
FROM
    Users
WHERE
    id = @Id
",
                new { Id = request.Id }
            );
            return result;
        }

        public GetUserResult GetUserByName(GetUserQuery request)
        {
            // example result of substr Id: 1->'001', 20->'020'
            var result = this.dbConnection.QueryFirstOrDefault<GetUserResult>(@"
SELECT
    substr('000'|| id, -3, 3) AS Id
    ,first_name AS FirstName
    ,family_name AS FamilyName
FROM
    Users
WHERE
    first_name = @FirstName
    AND family_name = @FamilyName
",
                new
                {
                    FirstName = request.FirstName,
                    FamilyName = request.FamilyName,
                }
            );
            return result;
        }
    }
}
