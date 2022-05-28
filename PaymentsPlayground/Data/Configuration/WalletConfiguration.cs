using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Data.Configuration
{
    internal class WalletConfiguration : EntityConfiguration<Wallet>
    {
        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasOne(x => x.User)
                .WithOne(x => x.Wallet)
                .HasForeignKey<Wallet>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Account)
                .IsRequired()
                .HasDefaultValueSql("1000");

            base.Configure(builder);
        }
    }
}
