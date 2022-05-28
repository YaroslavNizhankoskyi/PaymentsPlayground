using PaymentsPlayground.Data;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Services
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentProcessingService _paymentServcie;
        private readonly WalletConfiguration walletConfuguration;

        public WalletService(AppDbContext dbContext, 
            ICurrentUserService currentUserService,
            IPaymentProcessingService paymentServcie,
            WalletConfiguration walletConfuguration)
        {
            this._dbContext = dbContext;
            this._currentUserService = currentUserService;
            this._paymentServcie = paymentServcie;
            this.walletConfuguration = walletConfuguration;
        }

        public void CreateWallet(string userId)
        {
            var wallet = new Wallet
            {
                UserId = userId,
                Account = walletConfuguration.IntialBalance
            };

            _dbContext.Wallets.Add(wallet);

            _dbContext.SaveChanges();
        }

        public decimal GetBalance()
        {
            var user = _dbContext.Users
                .Where(x => x.UserName == _currentUserService.GetUserName())
                .FirstOrDefault();

            if (user == null) throw new ApplicationException();

            var wallet = _dbContext.Wallets
                .Where(x => x.UserId == user.Id)
                .FirstOrDefault();

            return wallet.Account;
        }

        public List<PaymentViewModel> GetOwnPayments()
        {
            return GetUserPayments(_currentUserService.GetUserName());
        }

        public List<PaymentViewModel> GetUserPayments(string email)
        {
            var listViewModels = new List<PaymentViewModel>();

            var user = _dbContext.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null) throw new ApplicationException("UserNotFound");

            var payments = _dbContext.UserPayments
                .Where(x => x.SenderId == user.Id || x.RecieverId == user.Id)
                .ToList();

            if (!payments.Any()) return listViewModels;

            var transactions = _dbContext.Transactions
                .Where(x => payments
                    .Select(x => x.Id)
                    .Contains(x.UserPaymentId))
                .ToList();

            foreach(var transaction in transactions)
            {
                var payment = payments
                    .FirstOrDefault(x => x.Id == transaction.UserPaymentId);

                var receiver = _dbContext.Users
                    .FirstOrDefault(x => x.Id == payment.RecieverId);

                var sender = _dbContext.Users
                    .FirstOrDefault(x => x.Id == payment.SenderId);

                listViewModels.Add(new PaymentViewModel
                {
                   Amount = payment.Amount,
                   DateFinished = transaction.FinishTime,
                   DateStarted = transaction.RegisterTime,
                   ReceiverName = receiver.UserName,
                   SenderName = sender.UserName,
                   Status = transaction.Status,
                   TransactionId = transaction.TransactionId
                });
            }

            return listViewModels;      
        }

        public List<string> RegisterSendMoney(PaymentDetails paymentDetails)
        {
            var reciever = _dbContext.Users
                .FirstOrDefault(x => x.Email == paymentDetails.UserEmail);

            var sender = _dbContext.Users
                .FirstOrDefault(x => x.UserName == _currentUserService.GetUserName());

            return _paymentServcie.CreatePayment(paymentDetails);
        }

        public void FinishPaymentSuccess(string transactionId)
        {
            var transaction = _dbContext.Transactions
                .FirstOrDefault(x => x.TransactionId == transactionId);

            var payment = _dbContext.UserPayments
                .FirstOrDefault(x => x.Id == transaction.UserPaymentId);

            var feeMoney = CalculateFee(payment.Amount);

            var transferMoney = payment.Amount - feeMoney;

            AddMoneyToUser(payment.RecieverId, transferMoney);
            SubtractMoneyFromUser(payment.SenderId, transferMoney);
            ProcessFee(feeMoney);
            _paymentServcie.MarkTransaction(transactionId, TransactionStatus.Sucessful);
        }

        public void FinishPaymentFailure(string transactionId, string errorDescription)
        {
            _paymentServcie.MarkTransactionFailureWithError(transactionId, errorDescription);
        }

        private void AddMoneyToUser(string userId, decimal amount)
        {
            var wallet = _dbContext.Wallets
                .FirstOrDefault(x => x.UserId == userId);

            wallet.Account += amount;

            _dbContext.SaveChanges();
        }

        private void SubtractMoneyFromUser(string userId, decimal amount)
        {
            var wallet = _dbContext.Wallets
                .FirstOrDefault(x => x.UserId == userId);

            wallet.Account -= amount;

            _dbContext.SaveChanges();
        }

        public decimal CalculateFee(decimal money)
        {
            return (money / 100) * walletConfuguration.FeePercentage;
        }

        private void ProcessFee(decimal fee)
        {
            var admin = _dbContext.Users
                .FirstOrDefault(x => x.Email == walletConfuguration.AdminEmail);

            AddMoneyToUser(admin.Id, fee);
        }


    }
}
