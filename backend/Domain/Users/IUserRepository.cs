namespace Domain.Users
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
    }
}
