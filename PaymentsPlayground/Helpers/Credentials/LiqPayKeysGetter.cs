using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment.Credentials;

namespace PaymentsPlayground.Helpers.Credentials
{
    public static class LiqPayKeysGetter
    {
        private const string Path = "wwwroot.res.liqpay.json";
        public static LiqPayCredentials Get()
        {
            return ResourceReader.Read<LiqPayCredentials>(Path);
        }
    }
}
