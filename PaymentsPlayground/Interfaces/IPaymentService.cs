using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Interfaces
{
    public interface IPaymentService
    {
        object GetPaymentForm(PaymentDetails paymentDetails);

        abstract PaymentType PaymentType { get; }
    }
}
