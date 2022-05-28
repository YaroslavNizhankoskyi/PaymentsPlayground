using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentsPlayground.Models;
using System.Reflection;

namespace PaymentsPlayground.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<UserPayment> UserPayments { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var domainAssembly = typeof(AppDbContext).Assembly;
            var softDeletableEntities = domainAssembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Entity)));

            foreach (var entityType in softDeletableEntities)
            {
                typeof(AppDbContext)
                .GetMethod("MakeSoftDeletable")
                ?.MakeGenericMethod(entityType)
                .Invoke(this, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }

        public void MakeSoftDeletable<T>(ModelBuilder modelBuilder) where T : class, ISoftDeletable
        {
            modelBuilder.Entity<T>()
                .HasQueryFilter(p => !p.IsDeleted)
                .Property(e => e.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        break;

                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
