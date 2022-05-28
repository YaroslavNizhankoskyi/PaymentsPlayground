using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using PaymentsPlayground.Helpers;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Payment;
using PaymentsPlayground.Models.ViewModels;
using PaymentsPlayground.Services;
using System.Text;
using ModelError = PaymentsPlayground.Models.ViewModels.ModelError;

namespace PaymentsPlayground.Pages
{
    [AllowAnonymous]
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPartialBuilderFactory _builderFactory;
        private readonly IWalletService _walletService;
        private readonly IUserValidatorService _userValidator;
        
        public PaymentModel PaymentModel { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPartialBuilderFactory builderFactory,
            IWalletService walletService, IUserValidatorService userValidator)
        {
            _logger = logger;
            this._builderFactory = builderFactory;
            this._walletService = walletService;
            this._userValidator = userValidator;
        }

        public void OnGet()
        {

        }

        public PartialViewResult OnPostRunPayment(PaymentModel model)
        {
            var validationResult = ValidatePaymentModel(model);

            if (validationResult.Any())
            {
                return ErrorPartialBuilder.Build(validationResult, ViewData);
            }

            var partialBuilder = _builderFactory.GetPartialBuilderByPaymentType(model.PaymentType);

            var paymentDetails = model.GetPaymentDetails();

            var partial = partialBuilder.BuildPaymentPartial(paymentDetails, ViewData);

            _walletService.RegisterSendMoney(paymentDetails);

            return partial;
        }

        private List<ModelError> ValidatePaymentModel(PaymentModel model)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => 
                    new ModelError
                    {
                        PropertyName = x.Key,
                        Error = x.Value.Errors.Select(x => x.ErrorMessage).First()
                    }
                ).ToList();

            if(_walletService.GetBalance() < model.Amount)
            {
                errors.Add(new ModelError
                {
                    PropertyName = "Amount",
                    Error = "Not enough money"
                });
            }

            if (!_userValidator.UserExists(model.UserEmail))
            {
                errors.Add(new ModelError
                {
                    PropertyName = "UserEmail",
                    Error = "No such user"
                });
            }
            else
            {
                if (_userValidator.HasSameEmailAsCurrentUser(model.UserEmail))
                {
                    errors.Add(new ModelError
                    {
                        PropertyName = "UserEmail",
                        Error = "Has same email as current user"
                    });
                }
            }
            
            return errors;
        }
    }
}