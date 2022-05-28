namespace PaymentsPlayground.Models.Payment.Credentials
{
    public class PayPalCredentials
    {
        public string Cmd { get; set; }

        public string Rm { get; set; }

        public string BusinessEmail { get; set; }

        public string NoShipping { get; set; }

        public string CurrencyCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}
