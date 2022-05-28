using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Interfaces
{
    public interface IUserLoginService
    {
        Task<List<string>> LoginOrRegister(UserLoginModel model);
    }
}
