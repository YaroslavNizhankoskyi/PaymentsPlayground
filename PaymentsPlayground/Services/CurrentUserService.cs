using PaymentsPlayground.Interfaces;

namespace PaymentsPlayground.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public string GetUserName() => _httpContextAccessor.HttpContext.User.Identity.Name;
    }
}
