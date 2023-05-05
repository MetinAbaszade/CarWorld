using AspReactTestApp.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace AspReactTestApp.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string Role { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpires { get; set; }

    }
}
