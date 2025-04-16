using Domain.Users;

namespace Persistence.Users
{
    [Repository]
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext dbContext;
        public UserRepository(UsersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
        public void Update(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }
    }
}
