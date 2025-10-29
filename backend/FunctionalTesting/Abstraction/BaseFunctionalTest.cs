namespace ExpenseTracker.FunctionalTesting.Abstraction;

public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    public BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected HttpClient HttpClient { get; }
}
