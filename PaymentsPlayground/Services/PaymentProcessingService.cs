using PaymentsPlayground.Data;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Services
{
    public class PaymentProcessingService : IPaymentProcessingService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public PaymentProcessingService(AppDbContext dbContext, ICurrentUserService currentUserService)
        {
            this._dbContext = dbContext;
            this._currentUserService = currentUserService;
        }

        public List<string> CreatePayment(PaymentDetails paymentDetails)
        {
            var errorList = new List<string>();

            var sender = _dbContext.Users
                .FirstOrDefault(x => x.UserName == _currentUserService.GetUserName());

            var receiver = _dbContext.Users
                .FirstOrDefault(x => x.Email == paymentDetails.UserEmail);

            if (receiver == null)
            {
                errorList.Add($"No user with email: {receiver.Email}");
            }

            var userPayment = new UserPayment
            {
                SenderId = sender.Id,
                RecieverId = receiver.Id,
                Amount = paymentDetails.Amount
            };

            _dbContext.UserPayments.Add(userPayment);
            if (_dbContext.SaveChanges() == 0) 
            {
                errorList.Add("Could not add payment");
                return errorList;
            }

            var transaction = new Transaction
            {
                UserPaymentId = userPayment.Id,
                RegisterTime = DateTimeOffset.Now,
                Status = TransactionStatus.Registered,
                TransactionId = paymentDetails.OrderId
            };

            _dbContext.Transactions.Add(transaction);
            if (_dbContext.SaveChanges() == 0)
            {
                errorList.Add("Could not add transaction");
                return errorList;
            }

            return errorList;
        }

        public void MarkTransactionFailureWithError(string transactionId, string errorDescription)
        {
            var transaction = _dbContext.Transactions.
                FirstOrDefault(x => x.TransactionId == transactionId);

            transaction.Status = TransactionStatus.Failure;
            transaction.ErrorDescription = errorDescription;
            transaction.FinishTime = DateTimeOffset.Now;

            _dbContext.SaveChanges();
        }

        public void MarkTransaction(string transactionId, TransactionStatus status)
        {
            var transaction = _dbContext.Transactions.
                FirstOrDefault(x => x.TransactionId == transactionId);

            transaction.Status = status;
            transaction.FinishTime = DateTimeOffset.Now;
            _dbContext.SaveChanges();
        }
    }
}
