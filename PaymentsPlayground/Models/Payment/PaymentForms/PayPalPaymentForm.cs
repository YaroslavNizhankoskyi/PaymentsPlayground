namespace PaymentsPlayground.Models.Payment.PaymentForms
{
    public class PayPalPaymentForm
    {
        public string Cmd { get; set; }

        public string Business { get; set; }

        public string Item_name { get; set; }

        public string Request_Id { get; set; }

        public string Amount { get; set; }

        public string No_shipping { get; set; }

        public string Return_url { get; set; }

        public string Rm { get; set; }

        public string Notify_url { get; set; }

        public string Cancel_url { get; set; }

        public string Currency_code { get; set; }

        public string Custom { get; set; }
    }
}
