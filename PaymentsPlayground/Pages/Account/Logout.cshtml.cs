using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsPlayground.Interfaces;

namespace PaymentsPlayground.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;

        public LogoutModel(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<IActionResult> OnGet()
        {
            await _authService.LogoutAsync();

            return Redirect("/Index");
        }
    }
}
