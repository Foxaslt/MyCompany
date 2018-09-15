using System.Threading;
using System.Threading.Tasks;

namespace MyCompany.ProductCatalog.Api.Database
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}
