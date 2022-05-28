using PaymentsPlayground.Models.Auth;

namespace PaymentsPlayground.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResult<string>> RegisterAsync(string password, string email);
        Task<AuthResult> RemoveUserAsync(string id);
        Task<AuthResult> AddToRoleAsync(string id, string roleName);
        Task<AuthResult> LoginAsync(string email, string password);

        Task LogoutAsync();
    }
}
