using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Factories
{
    public class PaymentServiceFactory : IPaymentServiceFactory
    {
        private readonly IDictionary<PaymentType, IPaymentService> _paymentService
                                                = new Dictionary<PaymentType, IPaymentService>();
        public PaymentServiceFactory(IServiceProvider serviceProvider)
        {
            var paymentServices = serviceProvider.GetServices<IPaymentService>();

            foreach (var service in paymentServices)
            {
                _paymentService[service.PaymentType] = service;
            }
        }

        public IPaymentService GetServiceByPaymentType(PaymentType type)
        {
            return _paymentService[type];
        }
    }
}
