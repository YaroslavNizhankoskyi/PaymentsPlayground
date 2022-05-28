using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Data.Configuration
{
    internal class UserPaymentConfiguration : EntityConfiguration<UserPayment>
    {
        public override void Configure(EntityTypeBuilder<UserPayment> builder)
        {
            builder.HasOne(x => x.Sender)
            .WithMany(x => x.SenderPayments)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.HasOne(x => x.Reciever)
                .WithMany(x => x.RecieverPayments)
                .HasForeignKey(x => x.RecieverId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            builder.Property(x => x.Amount)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
