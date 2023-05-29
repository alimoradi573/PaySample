﻿using Microsoft.EntityFrameworkCore;
using Pay.OvetimePolicies.Application.IDatabaseContexts;
using Pay.OvetimePolicies.Domain.Attributes;
using Pay.OvetimePolicies.Domain.Entities;
using System.Reflection;

namespace Pay.OvetimePolicies.Persistence.DatabaseContexts
{
    public class PayDbContext : DbContext, IPayDbContext
    { 
        public DbSet<PayEntity> Pays { get; set; }
        public PayDbContext(DbContextOptions<PayDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType
                    .GetCustomAttributes(
                    typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreateAt");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModifiedAt");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemovedAt");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");
                }
            }
 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

      
        public override int SaveChanges()
        {
            var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
              c.State == EntityState.Added ||
              c.State == EntityState.Deleted).ToList();
            foreach (var item in candidas)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var inserted = entityType.FindProperty("CreateAt");
                var modified = entityType.FindProperty("LastModifiedAt");
                var deleted = entityType.FindProperty("RemovedAt");
                var isRemoved = entityType.FindProperty("IsRemoved");
                if (item.State == EntityState.Added && inserted is not null)
                {
                    item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Modified && modified is not null)
                {
                    item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
                {
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
              c.State == EntityState.Added ||
              c.State == EntityState.Deleted).ToList();
            foreach (var item in candidas)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var inserted = entityType.FindProperty("CreateAt");
                var modified = entityType.FindProperty("LastModifiedAt");
                var deleted = entityType.FindProperty("RemovedAt");
                var isRemoved = entityType.FindProperty("IsRemoved");
                if (item.State == EntityState.Added && inserted is not null)
                {
                    item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Modified && modified is not null)
                {
                    item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
                {
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

            return await base.SaveChangesAsync();
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
