using Cas.Contact.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";

var app = builder.Build();

app.MapPost("/list/refresh", () =>
{
    var refreshService = new ListRefreshService();
    var refreshRequests = refreshService.GetContactRefreshMetadatas();

});

app.Run();
