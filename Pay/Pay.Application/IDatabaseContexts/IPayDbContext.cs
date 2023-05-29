using Microsoft.EntityFrameworkCore;
using Pay.OvetimePolicies.Domain.Entities;

namespace Pay.OvetimePolicies.Application.IDatabaseContexts
{
    public interface IPayDbContext
    {
        public DbSet<PayEntity> Pays { get; set; }
        public int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
