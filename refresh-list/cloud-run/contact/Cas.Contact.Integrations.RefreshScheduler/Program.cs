using Cas.Contact.Services;
using Cas.Core.Messages.Contact;
using Cas.Core.PlatformSettings;
using Cas.Core.Services.Gcp.PubSub;

using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";

builder.Logging.AddLog4Net();

builder.Services.Configure<GcpPubsubSettings>(
    config.GetSection($"{CasContactListRefreshSettings.Name}:{nameof(CasContactListRefreshSettings.GcpPubsub)}"));

builder.Services.AddScoped<PubsubService<RefreshListMessage>>();
builder.Services.AddScoped<ListRefreshService>();

var app = builder.Build();

app.MapGet(
    "/",
    () =>
    {
        return Results.Ok("Works!");
    });

app.MapPost(
    "/list/refresh",
    async (ILogger<Program> logger, ListRefreshService refreshService, PubsubService<RefreshListMessage> pubsubService) =>
{
    try
    {
        var refreshRequests = refreshService.GetContactRefreshMetadatas();

        var refreshMessages = refreshRequests.Select(
            req => new RefreshListMessage
            {
                CorrelationId = req.CorrelationId,
                Origin = "list-refresh-request-scheduler",
                CompanyId = req.CompanyId,
                ListId = req.ListId
            });

        await pubsubService.PublishAsync(refreshMessages);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error triggering refresh requests");
        return Results.Problem(JsonConvert.SerializeObject(ex));
    }

    return Results.Ok();
});

app.Run();
