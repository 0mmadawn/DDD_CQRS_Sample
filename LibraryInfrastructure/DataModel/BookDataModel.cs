using LibraryDomain.Models.Books;

namespace LibraryInfrastructure.DataModel
{
    internal class BookDataModel
    {
        internal string Id { get; }

        internal string Name { get; }

        internal BookDataModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        internal BookDataModel(Book source)
        {
            Id = source.Id;
            Name = source.Name;
        }

        internal Book ToDomainObject()
            => new Book(id: Id, name: Name);
    }
}
