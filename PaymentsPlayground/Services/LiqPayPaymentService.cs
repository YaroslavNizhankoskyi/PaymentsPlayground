using Newtonsoft.Json;
using PaymentsPlayground.Helpers.Credentials;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.Payment.Credentials;
using PaymentsPlayground.Models.Payment.PaymentForms;
using System.Security.Cryptography;
using System.Text;

namespace PaymentsPlayground.Services
{
    public class LiqPayPaymentService : IPaymentService
    {
        private readonly LiqPayCredentials _credentials;

        public PaymentType PaymentType => PaymentType.LiqPay;

        public LiqPayPaymentService()
        {
            this._credentials = LiqPayKeysGetter.Get();
        }

        private string GetLiqPaySignature(string data, string priveteKey)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(priveteKey + data + priveteKey)));
        }

        public object GetPaymentForm(PaymentDetails paymentDetails)
        {
            var signature_source = new LiqPayCheckout()
            {
                public_key = _credentials.PublicKey,
                version = 3,
                action = "pay",
                amount = paymentDetails.Amount,
                currency = "UAH",
                description = "Оплата замовлення",
                order_id = paymentDetails.OrderId,
                sandbox = 1,

                result_url = _credentials.ReturnUrl,
                server_url = _credentials.ServerUrl,

                product_category = "Напої",
                product_description = "Гаряче какао з альпійським молоком",
                product_name = "Гаряче какао"
            };
            var json_string = JsonConvert.SerializeObject(signature_source);
            var data_hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(json_string));
            var signature_hash = GetLiqPaySignature(data_hash, _credentials.PrivateKey);

            var model = new LiqPayPaymentForm();
            model.Data = data_hash;
            model.Signature = signature_hash;

            return model;
        }
    }
}
