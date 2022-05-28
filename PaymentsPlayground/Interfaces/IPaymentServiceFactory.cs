using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Interfaces
{
    public interface IPaymentServiceFactory
    {
        IPaymentService GetServiceByPaymentType(PaymentType type);
    }
}
