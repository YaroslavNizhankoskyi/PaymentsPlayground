using PaymentsPlayground.Data;
using PaymentsPlayground.Helpers.Const;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Services
{
    public class UserLoginService : IUserLoginService
    {
        private const string CouldNotLoginOrRegister = "Could not login or register";

        private readonly IAuthService _authService;
        private readonly AppDbContext _dbContext;
        private readonly IWalletService _walletService;

        public UserLoginService(IAuthService authService, AppDbContext dbContext, IWalletService walletService)
        {
            this._authService = authService;
            this._dbContext = dbContext;
            this._walletService = walletService;
        }

        public async Task<List<string>> LoginOrRegister(UserLoginModel model)
        {
            var loginResult = await _authService.LoginAsync(model.UserEmail, model.Password);

            if (loginResult.Succeeded) return new List<string>();

            return await Register(model);
        }


        public async Task<List<string>> Register(UserLoginModel model)
        {
            var registerResult = await _authService.RegisterAsync(model.Password, model.UserEmail);

            if (!registerResult.Succeeded) return registerResult.ErrorList;

            var addToRole = await _authService.AddToRoleAsync(registerResult.Result, RoleConstants.User);

            _walletService.CreateWallet(registerResult.Result);

            var repeatLoginResult = await _authService.LoginAsync(model.UserEmail, model.Password);

            return repeatLoginResult.ErrorList;
        }
    }
}
