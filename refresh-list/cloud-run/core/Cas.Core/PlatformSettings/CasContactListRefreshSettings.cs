namespace Cas.Core.PlatformSettings;

public class CasContactListRefreshSettings
{
    public const string Name = $"{CasContactSettings.Name}:ListRefresh";

    public GcpPubsubSettings GcpPubsub { get; set; }
}
