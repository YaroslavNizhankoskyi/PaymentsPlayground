using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Services
{
    public abstract class BasePaymentService : IPaymentService
    {
        public PaymentType PaymentType => PaymentType.LiqPay;

        public abstract object GetPaymentForm(PaymentDetails paymentDetails);
    }
}
