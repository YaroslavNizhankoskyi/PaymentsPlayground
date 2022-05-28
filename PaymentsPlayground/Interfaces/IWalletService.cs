using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Interfaces
{
    public interface IWalletService
    {
        public void CreateWallet(string userId);

        decimal GetBalance();

        void FinishPaymentSuccess(string transactionId);

        public void FinishPaymentFailure(string transactionId, string errorDescription);

        List<string> RegisterSendMoney(PaymentDetails paymentDetails);

        decimal CalculateFee(decimal money);

        public List<PaymentViewModel> GetOwnPayments();

        public List<PaymentViewModel> GetUserPayments(string email);
    }
}
    