using AspReactTestApp.Entities.Abstract;
using System.Linq.Expressions;
using System.Security.Principal;

namespace AspReactTestApp.Data.Core.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                          string includeProperties = "");

        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "");

        Task<List<TEntity>> GetListWithPagination(
                                    int? pageNumber,
                                    int? pageSize,
                                    Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "");

        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task DeleteAll();
    }
}
