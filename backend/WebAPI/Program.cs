using Persistence;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? throw new ArgumentNullException();
builder.Services.InitDatabase(connectionString);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseEntityFramework();

app.UseHttpsRedirection();

app.MapGet("api/v1/user/register", () => "Hello World!");
app.MapGet("api/v1/user/login", () => "Hello World!");

app.Run();