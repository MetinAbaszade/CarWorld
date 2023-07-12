using AspReactTestApp.Data.Core.Concrete.EntityFramework;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Entities.Concrete;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Data.DataAccess.Concrete.EntityFramework
{
    public class EfFueltypeDal : EfEntityRepositoryBase<Fueltype, AppDbContext>, IFueltypeDal
    {
    }
}
