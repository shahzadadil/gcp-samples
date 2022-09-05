namespace Cas.Core.Messages;

using System;

public class PlatformMessage
{
    public Guid CorrelationId { get; set; }
    public string Origin { get; set; }
}
