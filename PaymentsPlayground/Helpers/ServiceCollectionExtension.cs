using PaymentsPlayground.Factories;
using PaymentsPlayground.Helpers.Credentials;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.Payment.Credentials;
using PaymentsPlayground.Services;

namespace PaymentsPlayground.Helpers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserLoginService, UserLoginService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IPaymentProcessingService, PaymentProcessingService>();
            services.AddTransient<IUserValidatorService, UserValidatorService>();
            services.AddSingleton(typeof(LiqPayCredentials), LiqPayKeysGetter.Get());
            services.AddSingleton(typeof(PayPalCredentials), PayPalKeysGetter.Get());
            services.AddSingleton(typeof(WalletConfiguration), WalletConfigGetter.Get());

            services.AddTransient<IPaymentService, LiqPayPaymentService>();
            services.AddTransient<IPaymentService, PayPalPaymentService>();

            services.AddTransient<IPartialBuilder, LiqPayPartialBuilder>();
            services.AddTransient<IPartialBuilder, PayPalPartialBuilder>();

            services.AddTransient<IPartialBuilderFactory, PartialBuilderFactory>();
            services.AddTransient<IPaymentServiceFactory, PaymentServiceFactory>();

            return services;
        }
    }
}
