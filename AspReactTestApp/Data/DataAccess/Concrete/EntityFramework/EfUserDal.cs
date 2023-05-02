using AspReactTestApp.Data.Core.Concrete.EntityFramework;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete;

namespace AspReactTestApp.Data.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, AppDbContext>, IUserDal
    {
    }
}
