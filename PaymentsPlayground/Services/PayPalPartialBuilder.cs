using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.Payment.PaymentForms;

namespace PaymentsPlayground.Services
{
    public class PayPalPartialBuilder : IPartialBuilder
    {
        private const string PageName = "_PayPalPartial";

        private readonly IPaymentService _paymentService;

        public PaymentType PaymentType => PaymentType.PayPal;

        public PayPalPartialBuilder(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        public PartialViewResult BuildPaymentPartial(PaymentDetails paymentDetails, ViewDataDictionary dict)
        {
            return new PartialViewResult
            {
                ViewName = PageName,
                ViewData = new ViewDataDictionary<PayPalPaymentForm>(dict, _paymentService
                                                                               .GetPaymentForm(paymentDetails))
            };
        }
    }
}
