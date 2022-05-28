using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Data.Configuration
{
    internal class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, ISoftDeletable, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(e => e.IsDeleted).IsRequired();
        }
    }
}
