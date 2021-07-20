namespace LibraryApplication.Queries.Requets
{
    public class GetUserQuery
    {
        public string Id { get; }

        public string FirstName { get; }

        public string FamilyName { get; }

        public bool HasId()
            => !string.IsNullOrEmpty(Id);

        public GetUserQuery(string id)
        {
            Id = id;
        }

        public GetUserQuery(string firstName, string familyName)
        {
            FirstName = firstName;
            FamilyName = familyName;
        }
    }
}
