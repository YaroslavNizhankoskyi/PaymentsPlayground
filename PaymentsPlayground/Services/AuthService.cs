using Microsoft.AspNetCore.Identity;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;
using PaymentsPlayground.Models.Auth;

namespace PaymentsPlayground.Services
{
    public class AuthService : IAuthService
    {
        private const string UserNotFound = "User not found";
        private const string UserExists = "User already exists";
        private const string CouldNotSignIn = "Could not sign in";

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        public async Task<AuthResult> AddToRoleAsync(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return AuthResult.Error(UserNotFound);
            }
            else
            {
                var res = await _userManager.AddToRoleAsync(user, roleName);

                if (!res.Succeeded)
                {
                    var errors = res.Errors.Select(x => x.Description);

                    var authResult = new AuthResult();

                    return AuthResult.Error(errors);
                }

                return new AuthResult();
            }
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return AuthResult.Error(UserNotFound);
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, password, true, false);

            if (!result.Succeeded)
            {
                return AuthResult.Error(CouldNotSignIn);
            }

            return new AuthResult();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthResult<string>> RegisterAsync(string password, string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return AuthResult<string>.Error(UserExists);
            }

            var user = new User()
            {
                Email = email,
                UserName = email
            };

            var createUserResult = await _userManager.CreateAsync(user, password);

            if (!createUserResult.Succeeded)
            {
                var errors = createUserResult.Errors.Select(x => x.Description);

                return AuthResult<string>.Error(errors);
            }

            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(user, password, false);

            if (!signInResult.Succeeded)
            {
                return AuthResult<string>.Error(CouldNotSignIn);
            }

            return new AuthResult<string>(user.Id);
        }

        public async Task<AuthResult> RemoveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return AuthResult.Error(UserNotFound);
            }
            else
            {
                var res = await _userManager.DeleteAsync(user);

                if (!res.Succeeded)
                {
                    var errors = res.Errors.Select(x => x.Description);

                    return AuthResult.Error(errors);
                }

                return new AuthResult();
            }
        }
    }
}
