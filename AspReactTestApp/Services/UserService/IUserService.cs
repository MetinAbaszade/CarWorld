namespace AspReactTestApp.Services.UserService
{
    public interface IUserService
    {
        public Task<bool> CheckUserExists(string userName);
    }
}
