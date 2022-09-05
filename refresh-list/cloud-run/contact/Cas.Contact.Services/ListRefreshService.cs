using Cas.Contact.Data.Models;

namespace Cas.Contact.Services;

public class ListRefreshService
{

    public IEnumerable<ContactRefreshMetadata> GetContactRefreshMetadatas()
    {
        // TODO: in reality this would be build from DB
        IEnumerable<int> companies = Enumerable.Range(1, 5);
        IEnumerable<int> lists = Enumerable.Range(1, 5);
        var refreshRequests = new List<ContactRefreshMetadata>();

        Parallel.ForEach(
            companies,
            c => Parallel.ForEach(
                lists,
                l =>
                {
                    refreshRequests.Add(
                        new ContactRefreshMetadata
                        {
                            CorrelationId = Guid.NewGuid(),
                            CompanyId = c.ToString(),
                            ListId = l
                        });
                }));

        return refreshRequests;

    }
}
