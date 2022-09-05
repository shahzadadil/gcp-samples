namespace Cas.Core.Services.Gcp.PubSub;

using Cas.Core.Messages;
using Cas.Core.PlatformSettings;

using Google.Cloud.PubSub.V1;
using Google.Protobuf;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

public class PubsubService<T> where T : PlatformMessage
{
    private readonly ILogger _Logger;
    private readonly GcpPubsubSettings _Settings;

    public PubsubService(IOptions<GcpPubsubSettings> settings, ILogger<PubsubService<T>> logger)
    {
        ArgumentNullException.ThrowIfNull(settings);

        this._Logger = logger;
        this._Settings = settings.Value;
    }

    public async Task PublishAsync(IEnumerable<T> messages)
    {
        if (messages is null || !messages.Any())
        {
            this._Logger.LogWarning("No messages to publish");
            return;
        }

        var publisherClient = await PublisherServiceApiClient.CreateAsync();

        var publishMessages = messages.Select(
            message => new PubsubMessage
            {
                Attributes =
                {
                    { nameof(PlatformMessage.CorrelationId), message.CorrelationId.ToString() }
                },
                Data = ByteString.CopyFromUtf8(JsonConvert.SerializeObject(message))
            });

        // TODO: implement batching as there can be 100s of messages
        // TODO: send message one by one, and keep track of status
        // TODO: retry logic for message
        var publishResponse = await publisherClient.PublishAsync(this._Settings.TopicName, publishMessages);

        if (publishResponse is not null)
        {
            var messageIds = String.Join(",", publishResponse.MessageIds);
            this._Logger.LogTrace($"({publishResponse.MessageIds.Count}) messages of type (${nameof(T)}) published. Ids: {messageIds}");
        }
    }
}