using DataAnnotationsExtensions;
using PaymentsPlayground.Models.Payment;
using System.ComponentModel.DataAnnotations;

namespace PaymentsPlayground.Models.ViewModels
{
    public class PaymentModel
    {
        [Email]
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public PaymentType PaymentType { get; set; }

        public PaymentDetails GetPaymentDetails()
        {
            return new PaymentDetails
            {
                Amount = this.Amount,
                UserEmail = this.UserEmail,
                OrderId = Guid.NewGuid().ToString()
            };
        }
    }
}
