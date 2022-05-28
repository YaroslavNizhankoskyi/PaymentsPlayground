using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Pages.Account
{
    [BindProperties]
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IUserLoginService _userLoginService;

        public UserLoginModel UserLoginModel { get; set; }

        public LoginModel(IUserLoginService userLoginService)
        {
            this._userLoginService = userLoginService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _userLoginService.LoginOrRegister(UserLoginModel);

                TempData["message"] = "You've signed in successfully";

                return Redirect("/Index");
            }

            return Page();
        }
    }
}
