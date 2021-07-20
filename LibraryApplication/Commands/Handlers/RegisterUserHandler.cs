using LibraryApplication.Users.Commands.Requests;
using LibraryDomain.Models.Users;
using System.Transactions;

namespace LibraryApplication.Commands.Handlers
{
    public class RegisterUserHandler
    {
        private readonly IUserRepository userRepository;

        public RegisterUserHandler(IUserRepository repository)
        {
            userRepository = repository;
        }

        public void Handle(RegisterUserCommand request)
        {
            using (var transaction = new TransactionScope())
            {
                var user = new User(
                                userRepository.NextId(),
                                new UserName(request.FirstName, request.FamilyName)
                            );
                userRepository.Create(user);
                transaction.Complete();
            }
        }
    }
}
