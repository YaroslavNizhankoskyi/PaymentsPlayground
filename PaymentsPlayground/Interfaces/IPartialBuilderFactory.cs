using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Interfaces
{
    public interface IPartialBuilderFactory
    {
        public IPartialBuilder GetPartialBuilderByPaymentType(PaymentType paymentType);
    }
}
