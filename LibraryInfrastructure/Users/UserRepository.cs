using Dapper;
using LibraryDomain.Models.Users;
using LibraryInfrastructure.DataModel;
using LibraryInfrastructure.Shared;
using System;

namespace LibraryInfrastructure.Users
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Create(User user)
        {
            var userDataModel = new UserDataModel(user);
            this.dbConnection.Execute(
                "INSERT INTO Users VALUES (@Id, @FirstName, @FamilyName)",
                new
                {
                    Id = userDataModel.Id,
                    FirstName = userDataModel.FirstName,
                    FamilyName = userDataModel.FamilyName
                }
            );
        }

        public User Find(UserId id)
        {
           
            var result = this.dbConnection.QueryFirstOrDefault<UserDataModel>(@"
SELECT
    id AS Id
    ,first_name AS FirstName
    ,family_name AS FamilyName
FROM 
    Users
WHERE
    id = @Id
",
                new { Id = id.ToInt() }
            );
            var user = result.ToDomainObject();
            return user;
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public UserId NextId()
        {
            // if there is no existing sequence record, latestId value will be set to 0 by QueryFirstOrDefault
            var latestId = this.dbConnection.QueryFirstOrDefault<int>(
                "SELECT seq FROM SQLITE_SEQUENCE WHERE name = 'Users'"
            );

            var idString = string.Format("{0:000}", latestId+1);
            var result = new UserId(idString);
            return result;
        }
    }
}
