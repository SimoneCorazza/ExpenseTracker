using ExpenseTracker.Application.Users.Login;
using ExpenseTracker.Application.Users.Registration;
using System.Net.Http.Json;

namespace ExpenseTracker.FunctionalTesting.Abstraction
{
    public class BaseUserLoggedScenario : BaseFunctionalTest
    {
        public BaseUserLoggedScenario(FunctionalTestWebAppFactory factory)
            : base(factory)
        {

        }

        protected async Task<string> RegisterAndLogin(string email = "pippo@pluto.it", string password = "PippoPluto123!")
        {
            await HttpClient.PostAsJsonAsync(
                "api/v1/user/register",
                new RegisterRequest
                {
                    Email = email,
                    Password = password,
                });

            var response = await HttpClient.PostAsJsonAsync(
                "api/v1/user/login",
                new UserLoginRequest
                {
                    Email = email,
                    Password = password,
                });

            var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            return result.Token;
        }
    }
}
