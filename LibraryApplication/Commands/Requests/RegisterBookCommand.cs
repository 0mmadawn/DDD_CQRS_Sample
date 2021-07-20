namespace LibraryApplication.Commands.Requests
{
    public class RegisterBookCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Stock { get; set; }

        public RegisterBookCommand(string id, string name, int stock)
        {
            Id = id;
            Name = name;
            Stock = stock;
        }
    }
}
