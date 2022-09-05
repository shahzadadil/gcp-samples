namespace Cas.Core.Messages.Contact;

public class RefreshListMessage : PlatformMessage
{
    public string CompanyId { get; set; }
    public int ListId { get; set; }
}
