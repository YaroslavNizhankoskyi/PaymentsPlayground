using Microsoft.AspNetCore.Identity;

namespace PaymentsPlayground.Models
{
    public class User : IdentityUser
    {
        public Wallet Wallet { get; set; }

        public ICollection<UserPayment> RecieverPayments { get; set; }
        public ICollection<UserPayment> SenderPayments { get; set; }
    }
}
