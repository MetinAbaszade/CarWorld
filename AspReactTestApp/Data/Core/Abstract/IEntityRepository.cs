using AspReactTestApp.Entities.Abstract;
using System.Linq.Expressions;
using System.Security.Principal;

namespace AspReactTestApp.Data.Core.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null);

        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task DeleteAll();
    }
}
