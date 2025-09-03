using ExpenseTracker.Application.Services.EmailVerification;
using ExpenseTracker.Application.Services.ObjectStorage;
using ExpenseTracker.Application.Services.PasswordEncryptor;
using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Users.Services.PasswordValidator;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebAPI.API;
using ExpenseTracker.WebAPI.Configuration;
using Minio;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection") ?? throw new ArgumentNullException();
builder.Services.InitDatabase(connectionString);

builder.Services.AddSingleton<IEmailVerification>(sp => new EmailVerification(builder.Configuration.GetValue<int>("EmailBlobSize")));
builder.Services.AddSingleton<IPasswordEncryptor>(sp => new PasswordEncryptor(builder.Configuration.GetValue<int>("Password:SaltSize")));
builder.Services.AddSingleton<IPasswordValidator>(sp => new PasswordValidator(
    builder.Configuration.GetValue<int>("Password:Validation:MinimumLength"),
    builder.Configuration.GetValue<string>("Password:Validation:SpecialCharactersList")));

var objectStorageConfig = builder.Configuration.GetSection("ObjectStorage").Get<ObjectStorageConfig>();

builder.ConfigAuth();
builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("ExpenseTracker.Application")));
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor(); // Added to get the ClaimsPrincipal outside of the controller
builder.Services.AddMinio(x => x
    .WithEndpoint(objectStorageConfig.Endpoint)
    .WithCredentials(objectStorageConfig.AccessKey, objectStorageConfig.SecretKey)
.Build());

builder.Services.AddSingleton<IObjectStorage>(sp => new ObjectStorage(
    sp.GetRequiredService<IMinioClient>(),
    objectStorageConfig.BucketName));

builder.Services.AddScoped<IUser, User>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEntityFramework();
app.UseHttpsRedirection();

app.MapUsers();
app.MapCategories();
app.MapTransactions();


app.Run();