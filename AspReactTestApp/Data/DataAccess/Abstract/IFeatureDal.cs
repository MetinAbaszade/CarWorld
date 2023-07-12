using AspReactTestApp.Data.Core.Abstract;
using AspReactTestApp.Entities.Concrete.CarRelated;

namespace AspReactTestApp.Data.DataAccess.Abstract
{
    public interface IFeatureDal : IEntityRepository<Feature>
    {
    }
}
