using LibraryDomain.Models.Users;

namespace LibraryApplication.Users
{
    public class GetUserResult
    {
        public string Id { get; }

        public string FirstName { get; }

        public string FamilyName { get; }

        // for dapper
        public GetUserResult(){}

        public GetUserResult(User user)
        {
            Id = user.Id;
            FirstName = user.Name.FirstName;
            FamilyName = user.Name.FamilyName;
        }
    }
}
