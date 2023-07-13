using AspReactTestApp.Data.Core.Abstract;
using AspReactTestApp.Entities.Abstract;

namespace AspReactTestApp.Data.DataAccess.Abstract
{
    public interface IVehicleDetailRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
    }
}
