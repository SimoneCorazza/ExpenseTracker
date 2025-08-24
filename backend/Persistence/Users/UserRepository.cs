using ExpenseTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistWithEmail(string email)
        {
            return DbContext.Users.AnyAsync(u => u.Email.Address == email);
        }

        public Task<User?> GetByEmail(string email)
        {
            return DbContext.Users.SingleOrDefaultAsync(u => u.Email.Address == email);
        }
    }
