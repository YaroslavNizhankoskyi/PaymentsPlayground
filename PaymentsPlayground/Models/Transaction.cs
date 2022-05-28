namespace PaymentsPlayground.Models
{
    public class Transaction : Entity
    {
        public DateTimeOffset RegisterTime { get; set; }

        public DateTimeOffset FinishTime { get; set; }

        public TransactionStatus Status { get; set; }

        public string TransactionId { get; set; }

        public string ErrorDescription { get; set; }

        public Guid UserPaymentId { get; set; }

        public UserPayment UserPayment { get; set; }
    }

    public enum TransactionStatus
    {
        Registered,
        Failure,
        Sucessful,
        Reversed
    }
}
