using ExpenseTracker.Application.Services.Auth;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Application.Services.User
{
    public class User : IUser
    {
        public AuthenticatedUser? LoggedUser { get; }

        public User(IHttpContextAccessor httpContextAccessor, IAuth auth)
        {
            if (httpContextAccessor.HttpContext.User is null)
            {
                LoggedUser = null;
            }
            else
            {
                LoggedUser = auth.FromClaims(httpContextAccessor.HttpContext.User);
            }
        }
    }
}
