using System.Linq.Expressions;
using AspReactTestApp.Entities.Abstract;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace AspReactTestApp.Data.Core.Abstract;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
{
    Task<TEntity?> Get(Expression<Func<TEntity, bool>>? filter = null,
                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<List<TEntity>> GetListWithPagination(
                                int? pageNumber,
                                int? pageSize,
                                Expression<Func<TEntity, bool>>? filter = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<List<TDto>> GetListWithProjection<TDto>(
                                IMapper mapper,
                                int? pageNumber,
                                int? pageSize,
                                int languageId,
                                Expression<Func<TEntity, bool>>? filter = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    Task<List<TType>> Select<TType>(
                          Expression<Func<TEntity, bool>>? filter = null,
                          Expression<Func<TEntity, TType>>? select = null) where TType : class;

    Task<List<TType>> SelectWithPagination<TType>(
                  int? pageNumber,
                  int? pageSize,
                  Expression<Func<TEntity, bool>>? filter = null,
                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                  Expression<Func<TEntity, TType>>? select = null) where TType : class;

    Task<TType?> SelectSingle<TType>(
                        Expression<Func<TEntity, bool>>? filter = null,
                        Expression<Func<TEntity, TType>>? select = null) where TType : class;

    Task<TDto?> SelectSingleWithProjection<TDto>(
                        IMapper mapper,
                        int languageId,
                        Expression<Func<TEntity, bool>>? filter = null) where TDto : class;

    Task Add(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TEntity entity);

    Task DeleteAll();
}
