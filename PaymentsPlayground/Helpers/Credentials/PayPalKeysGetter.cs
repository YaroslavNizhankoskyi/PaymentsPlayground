using PaymentsPlayground.Models.Payment.Credentials;

namespace PaymentsPlayground.Helpers.Credentials
{
    public class PayPalKeysGetter
    {
        private const string Path = "wwwroot.res.paypal.json";
        public static PayPalCredentials Get()
        {
            return ResourceReader.Read<PayPalCredentials>(Path);
        }
    }
}
