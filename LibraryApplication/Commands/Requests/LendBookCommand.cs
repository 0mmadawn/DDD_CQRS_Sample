namespace LibraryApplication.Commands.Requests
{
    public class LendBookCommand
    {
        public string UserId { get; set; }

        public string BookId { get; set; }
        public LendBookCommand(string userId, string bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}
