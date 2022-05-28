namespace PaymentsPlayground.Models.Payment
{
    public class PaymentDetails
    {
        public string UserEmail { get; set; }

        public decimal Amount { get; set; }

        public string OrderId { get; set; }
    }
}
