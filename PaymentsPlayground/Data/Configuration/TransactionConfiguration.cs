using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Data.Configuration
{
    internal class TransactionConfiguration : EntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(x => x.UserPayment)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transaction>(x => x.UserPaymentId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.Property(x => x.ErrorDescription)
                .IsRequired(false)
                .HasMaxLength(1024);

            base.Configure(builder);
        }
    }
}
