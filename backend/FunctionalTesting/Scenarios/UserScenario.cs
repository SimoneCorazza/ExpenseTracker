using ExpenseTracker.Application.Users.Login;
using ExpenseTracker.Application.Users.Registration;
using ExpenseTracker.FunctionalTesting.Abstraction;
using System.Net;
using System.Net.Http.Json;

namespace ExpenseTracker.FunctionalTesting.Scenarios;

public class UserScenario : BaseFunctionalTest
{
    public UserScenario(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    private const string Email = "pippo@pluto.it";
    private const string Password = "PippoPluto123!";

    [Fact]
    public async Task User_registration_and_login()
    {
        await RegisterUser();
        await Login();
    }

    private async Task RegisterUser()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = Email,
            Password = Password,
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/user/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserRegistrationResponse>();
        Assert.NotNull(result);
        Assert.NotNull(result.UserId);
        Assert.Null(result.ErrorCode);
    }

    private async Task Login()
    {
        // Arrange
        var request = new UserLoginRequest
        {
            Email = Email,
            Password = Password,
        };

        // Act

        var response = await HttpClient.PostAsJsonAsync("api/v1/user/login", request);


        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserLoginResponse>();
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
        Assert.True(result.Success);
        Assert.NotNull(result.ExpireDate);
    }
}
