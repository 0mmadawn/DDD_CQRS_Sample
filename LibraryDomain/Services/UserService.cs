using LibraryDomain.Models.Users;

namespace LibraryDomain.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // TODO: use for duplicate check someday...

        public bool Exist(User user)
        {
            var findResult = userRepository.Find(user.Id);
            return findResult is not null;
        }

    }
}
