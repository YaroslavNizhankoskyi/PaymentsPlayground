using PaymentsPlayground.Data;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models;

namespace PaymentsPlayground.Services
{
    public class UserValidatorService : IUserValidatorService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public UserValidatorService(AppDbContext dbContext, ICurrentUserService currentUserService)
        {
            this._dbContext = dbContext;
            this._currentUserService = currentUserService;
        }

        public bool HasSameEmailAsCurrentUser(string email)
        {
            var user = GetUser(email);

            return user.UserName == _currentUserService.GetUserName();
        }

        public bool UserExists(string email)
        {
            return GetUser(email) != null;
        }
        private User GetUser(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
