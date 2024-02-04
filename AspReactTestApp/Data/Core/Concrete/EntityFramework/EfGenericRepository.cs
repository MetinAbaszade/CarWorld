using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AspReactTestApp.Data.Core.Abstract;
using AspReactTestApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using AutoMapper;
using TurboazFetching.Entities;

namespace AspReactTestApp.Data.Core.Concrete.EntityFramework;

public class EfGenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public async Task Add(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await context.SaveChangesAsync();
    }

    public async Task Delete(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAll()
    {
        using var context = new TContext();
        var allEntities = context.Set<TEntity>().ToList();
        context.RemoveRange(allEntities);
        await context.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        using var context = new TContext();
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null,
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
        {
            query = include(query);
        }
        return await query.SingleOrDefaultAsync(filter);
    }

    public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null,
                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<List<TType>> Select<TType>(Expression<Func<TEntity, bool>>? filter = null,
                                                 Expression<Func<TEntity, TType>>? select = null) where TType : class
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (select != null)
        {
            return await query.Select(select).ToListAsync();
        }

        return await query.Cast<TType>().ToListAsync();
    }

    public async Task<TType?> SelectSingle<TType>(Expression<Func<TEntity, bool>>? filter = null,
                                                  Expression<Func<TEntity, TType>>? select = null) where TType : class
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (select is null)
        {
            return null;
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (select != null)
        {
            return await query.Select(select).FirstOrDefaultAsync();
        }

        return await query.Cast<TType>().FirstOrDefaultAsync();
    }

    public async Task<TDto?> SelectSingleWithProjection<TDto>(IMapper mapper,
                                                              int languageId,
                                                              Expression<Func<TEntity, bool>>? filter = null) where TDto : class
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await mapper.ProjectTo<TDto>(query, new { languageId }).FirstOrDefaultAsync();
    }

    public async Task<List<TType>> SelectWithPagination<TType>(int? pageNumber,
                                                             int? pageSize,
                                                             Expression<Func<TEntity, bool>>? filter = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                             Expression<Func<TEntity, TType>>? select = null) where TType : class
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (select != null)
        {
            return await query.Select(select).ToListAsync();
        }

        return await query.Cast<TType>().ToListAsync();
    }

    public async Task<List<TEntity>> GetListWithPagination(
                                        int? pageNumber,
                                        int? pageSize,
                                        Expression<Func<TEntity, bool>>? filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        string test = "dakjfadskf";

        var queryString = query.ToQueryString(); // EF Core 5.0 and later
        Console.WriteLine(queryString);
        return await query.ToListAsync();
    }

    public async Task<List<TDto>> GetListWithProjection<TDto>(IMapper mapper,
                                                              int? pageNumber,
                                                              int? pageSize,
                                                              int languageId,
                                                              Expression<Func<TEntity, bool>>? filter = null,
                                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        using var context = new TContext();
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }   
        
        return await mapper.ProjectTo<TDto>(query, new { languageId }).ToListAsync();
    }

}
