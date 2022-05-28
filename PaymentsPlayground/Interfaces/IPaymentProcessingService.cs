using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Interfaces
{
    public interface IPaymentProcessingService
    {
        public List<string> CreatePayment(PaymentDetails paymentDetails);

        void MarkTransactionFailureWithError(string transactionId, string errorDescription);

        void MarkTransaction(string transactionId, TransactionStatus status);
    }
}
