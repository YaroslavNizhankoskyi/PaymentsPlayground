using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PaymentsPlayground.Helpers;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Pages
{
    [Authorize]
    public class GetUserPaymentsModel : PageModel
    {
        private readonly IWalletService _walletService;
        private readonly IUserValidatorService _userValidator;

        [TempData]
        public string Email { get; set; }

        public bool isAdmin => User.IsInRole("Admin");

        public List<PaymentViewModel> Payments { get; set; }


        public GetUserPaymentsModel(IWalletService walletService, IUserValidatorService userValidator)
        {
            this._walletService = walletService;
            this._userValidator = userValidator;
        }

        public void OnGet()
        {
            if (!isAdmin)
            {
                Payments = _walletService.GetOwnPayments();
            }
        }

        public PartialViewResult OnPostUserPayments(GetUserPaymentsViewModel model)
        {
            var validationResult = ValidatePaymentModel(model);

            if (validationResult.Any())
            {
                return ErrorPartialBuilder.Build(validationResult, ViewData);
            }

            Payments = _walletService.GetUserPayments(model.UserEmail);

            return new PartialViewResult
            {
                ViewName = "_PaymentsPartial",
                ViewData = new ViewDataDictionary<List<PaymentViewModel>>(ViewData, Payments)
            };
        }

        private List<ModelError> ValidatePaymentModel(GetUserPaymentsViewModel model)
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
