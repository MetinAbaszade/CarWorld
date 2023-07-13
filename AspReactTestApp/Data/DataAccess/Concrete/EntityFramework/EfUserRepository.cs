using AspReactTestApp.Data.Core.Concrete.EntityFramework;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Data.DataAccess.Concrete.EntityFramework
{
    public class EfUserRepository : EfGenericRepository<User, AppDbContext>, IUserRepository
    {
    }
}
