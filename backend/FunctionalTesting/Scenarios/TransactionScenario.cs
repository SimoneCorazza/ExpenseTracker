using ExpenseTracker.Application.Categories.Edit;
using ExpenseTracker.Application.Categories.Get;
using ExpenseTracker.Application.Places.Create;
using ExpenseTracker.Application.Transactions.Create;
using ExpenseTracker.Application.Transactions.Edit;
using ExpenseTracker.Application.Transactions.Get;
using ExpenseTracker.FunctionalTesting.Abstraction;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ExpenseTracker.FunctionalTesting.Scenarios;

public class TransactionScenario : BaseUserLoggedScenario
{
    private readonly string token;
    private readonly Guid idCategory;
    private readonly Guid idPlace;

    public TransactionScenario(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
        token = RegisterAndLogin().GetAwaiter().GetResult();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Categories
        var categories = new EditCategoriesRequest
        {
            RootCategories =
            [
                new ()
                {
                    Name = "General",
                    Description = "General expenses",
                    Childrens = [],
                },
            ],
        };
        HttpClient.PostAsJsonAsync("api/v1/categories", categories).GetAwaiter().GetResult();
        var response = HttpClient.GetAsync("api/v1/categories").GetAwaiter().GetResult();
        response.IsSuccessStatusCode.Should().BeTrue();
        var categoryResult = response.Content.ReadFromJsonAsync<GetCategoriesResponse>().GetAwaiter().GetResult();
        categoryResult.Should().NotBeNull();
        idCategory = categoryResult.Categories.Single().Id;

        // Places
        var place = new CreatePlace
        {
            Name = "Test Place",
            Description = "Test Place",
        };
        var result = HttpClient.PostAsJsonAsync("api/v1/places", place).GetAwaiter().GetResult();
        result.IsSuccessStatusCode.Should().BeTrue();
        var placeResult = result.Content.ReadFromJsonAsync<CreatePlaceResponse>().GetAwaiter().GetResult();
        placeResult.Should().NotBeNull();
        idPlace = placeResult.Id;
    }

    [Fact]
    public async Task User_adds_edit_and_delete_transaction()
    {
        await Should_return_empty_transactions_at_first_login();
        var id = await Should_add_a_transaction_with_no_place_and_category();
        await Should_edit_a_transaction(id);
        await Should_add_attachments_to_a_transaction(id);
        await Should_delete_a_transaction(id);
    }

    private async Task Should_return_empty_transactions_at_first_login()
    {
        // Arrange
        // Act
        var transactions = await GetTransactions();

        // Assert
        transactions.Transactions.Should().BeEmpty();
    }

    private async Task<Guid> Should_add_a_transaction_with_no_place_and_category()
    {
        // Arrange
        var request = new CreateTransactionRequest
        {
            Amount = 100.0m,
            CategoryId = null,
            PlaceId = null,
            Description = "Test Transaction",
            Date = new DateOnly(2021, 1, 4),
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/transactions", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CreateTransactionResponse>();
        result.Should().NotBeNull();
        result.Id.Should().NotBe(Guid.Empty);

        var r = await GetTransactions();
        r.Transactions.Should().SatisfyRespectively(first =>
        {
            first.Amount.Should().Be(request.Amount);
            first.CategoryId.Should().Be(request.CategoryId);
            first.PlaceId.Should().Be(request.PlaceId);
            first.Description.Should().Be(request.Description);
            first.Date.Should().Be(request.Date);
            first.Id.Should().Be(result.Id); // Same ID as the one returned on creation
        });

        return result.Id;
    }

    private async Task Should_edit_a_transaction(Guid id)
    {
        // Arrange
        var request = new EditTransactionRequest
        {
            Id = id,
            Amount = 90.0m,
            CategoryId = idCategory,
            PlaceId = idPlace,
            Description = "Edted Test Transaction",
            Date = new DateOnly(2025, 4, 10),
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync("api/v1/transactions", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var r = await GetTransactions();
        r.Transactions.Should().SatisfyRespectively(first =>
        {
            first.Id.Should().Be(id);
            first.Amount.Should().Be(request.Amount);
            first.CategoryId.Should().Be(request.CategoryId);
            first.PlaceId.Should().Be(request.PlaceId);
            first.Description.Should().Be(request.Description);
            first.Date.Should().Be(request.Date);
        });
    }

    private async Task Should_add_attachments_to_a_transaction(Guid id)
    {
        // Arrange
        var httpContent = new MultipartFormDataContent
        {
            { TestContent(), "PNG", "attachment1.png" },
            { TestContent(), "PDF", "attachment2.pdf" },
            { TestContent(), "JPG", "attachment3.jpg" },
        };

        // Act
        var response = await HttpClient.PostAsync($"api/v1/transactions/attachments/{id}", httpContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var r = await GetTransactions();
        r.Transactions.Should().ContainSingle();
        // TODO: check attachments
    }

    private static ByteArrayContent TestContent()
    {
        var content = new ByteArrayContent([1, 2]);
        content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        return content;
    }

    private async Task Should_delete_a_transaction(Guid id)
    {
        // Arrange
        // Act
        var response = await HttpClient.DeleteAsync($"api/v1/transactions/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var r = await GetTransactions();
        r.Transactions.Should().BeEmpty();
    }

    private async Task<GetTransactionsResponse> GetTransactions()
    {
        var response = await HttpClient.GetAsync("api/v1/transactions");
        response.IsSuccessStatusCode.Should().BeTrue();
        var transactions = await response.Content.ReadFromJsonAsync<GetTransactionsResponse>();
        transactions.Should().NotBeNull();
        return transactions!;
    }
}
