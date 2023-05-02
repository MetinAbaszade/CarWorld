using AspReactTestApp.Entities.Abstract;

namespace AspReactTestApp.Entities.Concrete
{
    public class RefreshToken : IEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
