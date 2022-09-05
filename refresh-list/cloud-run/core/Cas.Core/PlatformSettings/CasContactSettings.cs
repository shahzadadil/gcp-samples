namespace Cas.Core.PlatformSettings;

public class CasContactSettings
{
    public const string Name = $"{CasSettings.Name}:Contact";

    public CasContactListRefreshSettings CasContactListRefreshSettings { get; set; }
}
