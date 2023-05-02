using AspReactTestApp.Data.DataAccess.Abstract;
using System.Security.Claims;

namespace AspReactTestApp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserDal userDal, IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CheckUserExists(string userName)
        {
            var user = await _userDal.Get(user => user.UserName == userName);
            return user != null;
        }

        public string GetUserRole()
        {
            var result = string.Empty;
            result = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return result;
        }
    }
}
