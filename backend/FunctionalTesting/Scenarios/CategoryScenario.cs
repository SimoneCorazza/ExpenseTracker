using ExpenseTracker.Application.Categories.Edit;
using ExpenseTracker.Application.Categories.Get;
using ExpenseTracker.FunctionalTesting.Abstraction;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ExpenseTracker.FunctionalTesting.Scenarios;

public class CategoryScenario : BaseUserLoggedScenario
{
    private readonly string token;

    public CategoryScenario(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
        token = RegisterAndLogin().GetAwaiter().GetResult();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task User_edit_and_get_categories()
    {
        await Should_return_empty_at_first_user_login();
        await Should_add_categories();
        await Should_remove_and_edit_categories();

    }

    public async Task Should_return_empty_at_first_user_login()
    {
        // Act
        var categories = await GetCategories();

        // Assert
        Assert.Empty(categories.Categories);
    }

    public async Task Should_add_categories()
    {
        // Arrange
        var request = new EditCategoriesRequest
        {
            RootCategories =
            [
                new ()
                {
                    Name = "House",
                    Description = "House related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "Rent",
                            Description = "Monthly rent",
                            Childrens = [],
                        },
                        new ()
                        {
                            Name = "Utilities",
                            Description = "Electricity, water, gas, etc.",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Name = "Car",
                    Description = "Car related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "Fuel",
                            Description = "Fuel for the car",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Name = "I will be removed",
                    Description = "Remuved",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "I also will be removed",
                            Description = "Remuved",
                            Childrens = [],
                        },
                    ],
                },
            ],
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/categories", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var categories = await GetCategories();
        categories.Should().BeEquivalentTo(new GetCategoriesResponse
        {
            Categories =
            [
                new ()
                {
                    Name = "House",
                    Description = "House related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "Rent",
                            Description = "Monthly rent",
                            Childrens = [],
                        },
                        new ()
                        {
                            Name = "Utilities",
                            Description = "Electricity, water, gas, etc.",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Name = "Car",
                    Description = "Car related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "Fuel",
                            Description = "Fuel for the car",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Name = "I will be removed",
                    Description = "Remuved",
                    Childrens =
                    [
                        new ()
                        {
                            Name = "I also will be removed",
                            Description = "Remuved",
                            Childrens = [],
                        },
                    ],
                },
            ],
        }, options => options.Excluding(x => x.Name == "Id"));

    }

    public async Task Should_remove_and_edit_categories()
    {
        // Arrange
        var categories = await GetCategories();
        var house = categories.Categories.Single(c => c.Name == "House");
        var car = categories.Categories.Single(c => c.Name == "Car");
        var request = new EditCategoriesRequest
        {
            RootCategories =
            [
                new ()
                {
                    Id = house.Id,
                    Name = "House",
                    Description = "House related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Id = house.Childrens.Single(x => x.Name == "Rent").Id,
                            Name = "Rent",
                            Description = "Monthly rent",
                            Childrens = 
                            [
                                new ()
                                {
                                    Name = "New Sub Sub category",
                                    Description = "Sub Sub category",
                                    Childrens = [],
                                },
                            ],
                        },
                        new ()
                        {
                            Id = house.Childrens.Single(x => x.Name == "Utilities").Id,
                            Name = "Utilities",
                            Description = "Electricity, water, gas, etc.",
                            Childrens = [],
                        },
                        new ()
                        {
                            Name = "New Sub category",
                            Description = "Sub category",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Id = car.Id,
                    Name = "Vehicle",
                    Description = "Vehicle related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Id = car.Childrens.Single(x => x.Name == "Fuel").Id,
                            Name = "Fuel",
                            Description = "Fuel for the car",
                            Childrens = [],
                        },
                    ],
                },
            ],
        };


        // Act
        var response = await HttpClient.PostAsJsonAsync("api/v1/categories", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var newCategories = await GetCategories();
        var newHouse = newCategories.Categories.Single(c => c.Name == "House");
        newCategories.Should().BeEquivalentTo(new GetCategoriesResponse
        {
            Categories =
            [
                new ()
                {
                    Id = house.Id,
                    Name = "House",
                    Description = "House related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Id = house.Childrens.Single(x => x.Name == "Rent").Id,
                            Name = "Rent",
                            Description = "Monthly rent",
                            Childrens =
                            [
                                new ()
                                {
                                    Id = newHouse.Childrens.Single(x => x.Name == "Rent").Childrens.Single(y => y.Name == "New Sub Sub category").Id,
                                    Name = "New Sub Sub category",
                                    Description = "Sub Sub category",
                                    Childrens = [],
                                },
                            ],
                        },
                        new ()
                        {
                            Id = house.Childrens.Single(x => x.Name == "Utilities").Id,
                            Name = "Utilities",
                            Description = "Electricity, water, gas, etc.",
                            Childrens = [],
                        },
                        new ()
                        {
                            Id = newHouse.Childrens.Single(x => x.Name == "New Sub category").Id,
                            Name = "New Sub category",
                            Description = "Sub category",
                            Childrens = [],
                        },
                    ],
                },
                new ()
                {
                    Id = car.Id,
                    Name = "Vehicle",
                    Description = "Vehicle related expenses",
                    Childrens =
                    [
                        new ()
                        {
                            Id = car.Childrens.Single(x => x.Name == "Fuel").Id,
                            Name = "Fuel",
                            Description = "Fuel for the car",
                            Childrens = [],
                        },
                    ],
                },
            ],
        });

    }

    private async Task<GetCategoriesResponse> GetCategories()
    {
        var response = await HttpClient.GetAsync("api/v1/categories");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);


        return (await response.Content.ReadFromJsonAsync<GetCategoriesResponse>())!;
    }
}
