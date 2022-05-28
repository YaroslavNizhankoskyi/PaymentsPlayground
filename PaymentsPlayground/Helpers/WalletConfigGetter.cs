using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Helpers
{
    public class WalletConfigGetter
    {
        private const string Path = "wwwroot.res.wallet.config.json";
        public static WalletConfiguration Get()
        {
            return ResourceReader.Read<WalletConfiguration>(Path);
        }
    }
}
