namespace Cas.Core.Services.Gcp.PubSub;

public abstract class PubSubService<T>
{
    public abstract string TopicName { get; }

    public abstract Task PublishAsync(T message);
}