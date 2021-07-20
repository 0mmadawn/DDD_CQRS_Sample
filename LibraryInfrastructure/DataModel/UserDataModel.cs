using LibraryDomain.Models.Users;

namespace LibraryInfrastructure.DataModel
{
    internal class UserDataModel
    {
        internal string Id { get; }

        internal string FirstName { get; }

        internal string FamilyName { get; }

        internal UserDataModel(string id, string firstName, string familyName)
        {
            Id = id;
            FirstName = firstName;
            FamilyName = familyName;
        }

        internal UserDataModel(User source)
        {
            Id = source.Id;
            FirstName = source.Name.FirstName;
            FamilyName = source.Name.FamilyName;
        }

        internal User ToDomainObject()
        {
            var userName = new UserName(FirstName, FamilyName);
            return new User(Id, userName);
        }
    }
}
