namespace PaymentsPlayground.Models.Payment.Credentials
{
    public class LiqPayCredentials
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        public string ServerUrl { get; set; }

        public string ReturnUrl { get; set; }
    }
}
