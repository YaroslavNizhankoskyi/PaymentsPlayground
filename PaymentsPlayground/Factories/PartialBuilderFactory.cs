using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment;

namespace PaymentsPlayground.Factories
{
    public class PartialBuilderFactory : IPartialBuilderFactory
    {
        private readonly IDictionary<PaymentType, IPartialBuilder> _partialBuilders = new Dictionary<PaymentType, IPartialBuilder>();

        public PartialBuilderFactory(IPaymentServiceFactory paymentFactory, IServiceProvider serviceProvider)
        {
            var builderServices = serviceProvider.GetServices<IPartialBuilder>();

            foreach (var builder in builderServices)
            {
                var paymentType = builder.PaymentType;

                var paymentService = paymentFactory.GetServiceByPaymentType(paymentType);

                var type = builder.GetType();

                var requiredBuilder = (IPartialBuilder)Activator.CreateInstance(type, paymentService);

                _partialBuilders.Add(paymentType, requiredBuilder);

            }
        }

        public IPartialBuilder GetPartialBuilderByPaymentType(PaymentType paymentType)
        {
            return _partialBuilders[paymentType];
        }
    }
}
