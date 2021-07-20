namespace LibraryDomain.Models.Users
{
    public interface IUserRepository
    {
        void Create(User user);
        User Find(UserId id);
        void Delete(User user);
        UserId NextId();
    }
}
