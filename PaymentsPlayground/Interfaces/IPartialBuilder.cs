using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Interfaces
{
    public interface IPartialBuilder
    {
        public PaymentType PaymentType { get; }
        PartialViewResult BuildPaymentPartial(PaymentDetails paymentDetails, ViewDataDictionary dict);
    }
}

