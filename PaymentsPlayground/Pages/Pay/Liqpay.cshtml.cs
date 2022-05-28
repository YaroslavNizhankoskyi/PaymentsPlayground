using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Pages.Pay
{
    public class LiqpayModel : PageModel
    {
        public LiqPayCheckoutModel CheckOut { get; set; }
        public ActionResult OnGetPay(LiqPayCheckoutModel model)
        {
            CheckOut = model;
            return Page();
        }

    }
}
