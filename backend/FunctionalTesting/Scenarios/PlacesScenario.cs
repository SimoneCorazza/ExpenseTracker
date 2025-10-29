using ExpenseTracker.Application.Places.Create;
using ExpenseTracker.Application.Places.Edit;
using ExpenseTracker.Application.Places.Get;
using ExpenseTracker.FunctionalTesting.Abstraction;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ExpenseTracker.FunctionalTesting.Scenarios;

public class PlacesScenario : BaseUserLoggedScenario
{
    private readonly string token;

    public PlacesScenario(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
        token = RegisterAndLogin().GetAwaiter().GetResult();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task User_edit_and_gets_the_places()
    {
        await Should_return_empty_places_at_first_login();

        Guid id = await Should_add_place();

        await Should_edit_place(id);

        await Should_delete_place(id);
    }

    private async Task Should_return_empty_places_at_first_login()
    {
        // Act
        var places = await GetPlaces();

        // Assert
        Assert.Empty(places.Places);
    }

    private async Task<Guid> Should_add_place()
    {
        // Arrange
        var request = new CreatePlace
        {
            Name = "Test Place",
            Description = "This is a test place",
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/places", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreatePlaceResponse>();
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);

        var places = await GetPlaces();
        Assert.Single(places.Places);
        var p = places.Places.Single();
        Assert.Equal(request.Name, p.Name);
        Assert.Equal(request.Description, p.Description);

        return result.Id;
    }

    private async Task Should_delete_place(Guid id)
    {
        // Arrange
        // Act
        var response = await HttpClient.DeleteAsync($"api/v1/places/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedPlaces = await GetPlaces();
        Assert.Empty(updatedPlaces.Places);
    }

    private async Task Should_edit_place(Guid id)
    {
        // Arrange
        var request = new EditPlace
        {
            Id = id,
            Name = "Updated Test Place",
            Description = "This is an updated test place",
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync("api/v1/places", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var places = await GetPlaces();
        Assert.Single(places.Places);
        var p = places.Places.Single();
        Assert.Equal(request.Name, p.Name);
        Assert.Equal(request.Description, p.Description);
    }

    private async Task<GetPlacesResponse> GetPlaces()
    {
        // Act
        var response = await HttpClient.GetAsync("api/v1/places");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<GetPlacesResponse>();
        Assert.NotNull(result);
        return result;
    }
}
