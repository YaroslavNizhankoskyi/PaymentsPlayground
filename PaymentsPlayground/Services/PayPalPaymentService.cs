using Microsoft.Extensions.Options;
using PaymentsPlayground.Helpers;
using PaymentsPlayground.Helpers.Credentials;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.Payment.Credentials;
using PaymentsPlayground.Models.Payment.PaymentForms;
using PayPal.Api;

namespace PaymentsPlayground.Services
{
    public class PayPalPaymentService : IPaymentService
    {
        private readonly PayPalCredentials _credentials;

        public PaymentType PaymentType => PaymentType.PayPal;

        public PayPalPaymentService()
        {
            this._credentials = PayPalKeysGetter.Get();
        }

        public object GetPaymentForm(PaymentDetails paymentDetails)
        {
            return new PayPalPaymentForm
            {
                Amount = paymentDetails.Amount.ToString(),
                Custom = paymentDetails.OrderId.ToString(),
                Request_Id = paymentDetails.OrderId.ToString(),
                Business = _credentials.BusinessEmail,
                Rm = _credentials.Rm,
                Cmd = _credentials.Cmd,
                Currency_code = _credentials.CurrencyCode,
                No_shipping = _credentials.NoShipping,
                Cancel_url = "",
                Notify_url = "",
                Return_url = "https://b894-195-170-179-28.eu.ngrok.io/api/callbacks/paypal",
                Item_name = "Transaction"
            };
        }
    }
}
