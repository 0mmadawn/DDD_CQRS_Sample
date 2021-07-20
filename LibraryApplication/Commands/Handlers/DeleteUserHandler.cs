using LibraryDomain.Models.Users;
using System.Transactions;

namespace LibraryApplication.Commands.Handlers
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository userRepository;

        public DeleteUserHandler(IUserRepository repository)
        {
            userRepository = repository;
        }

        public void Handle(string userId)
        {
            using (var transaction = new TransactionScope())
            {
                User user = userRepository.Find(userId);
                if (user is null) { return; }
                userRepository.Delete(user);
                transaction.Complete();
            }
        }
    }
}
