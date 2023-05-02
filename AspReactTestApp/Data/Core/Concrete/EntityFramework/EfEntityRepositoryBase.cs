using AspReactTestApp.Data.Core.Abstract;
using AspReactTestApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspReactTestApp.Data.Core.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                try
                {
                    var test = context.Set<TEntity>().ToList();
                }
                catch (Exception ex)
                {
                    var testtt = ex.Message;
                }
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll()
        {
            using (var context = new TContext())
            {
                var allEntities = context.Set<TEntity>().ToList();
                context.RemoveRange(allEntities);
                await context.SaveChangesAsync();
            }
        }
    }
}
