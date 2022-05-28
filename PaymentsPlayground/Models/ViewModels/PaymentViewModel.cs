namespace PaymentsPlayground.Models.ViewModels
{
    public class PaymentViewModel
    {
        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public string TransactionId { get; set; }

        public decimal Amount { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTimeOffset DateFinished { get; set; }

        public DateTimeOffset DateStarted { get; set; }
    }
}
