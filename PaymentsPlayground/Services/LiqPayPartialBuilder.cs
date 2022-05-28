using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.Payment.PaymentForms;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Services
{
    public class LiqPayPartialBuilder : IPartialBuilder
    {
        private const string PageName = "_LiqPayPartial";

        private readonly IPaymentService _paymentService;

        public PaymentType PaymentType => PaymentType.LiqPay;

        public LiqPayPartialBuilder(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        public PartialViewResult BuildPaymentPartial(PaymentDetails paymentDetails, ViewDataDictionary dict)
        {
            return new PartialViewResult
            {
                ViewName = PageName,
                ViewData = new ViewDataDictionary<LiqPayPaymentForm>(dict, _paymentService
                                                                                .GetPaymentForm(paymentDetails))
            };
        }
    }
}
