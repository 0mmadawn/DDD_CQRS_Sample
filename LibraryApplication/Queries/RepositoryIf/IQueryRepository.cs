using LibraryApplication.Queries.Requets;
using LibraryApplication.Queries.Results;
using LibraryApplication.Users;
using System.Collections.Generic;

namespace LibraryApplication.Queries.RepositoryIf
{
    public interface IQueryRepository
    {
        IEnumerable<GetAllBooksResult> GetAllBooks();
        GetUserResult GetUserById(GetUserQuery request);
        GetUserResult GetUserByName(GetUserQuery request);
        IEnumerable<GetAllUsersResult> GetAllUser();
    }
}
