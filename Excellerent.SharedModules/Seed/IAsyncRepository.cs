using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.SharedModules.Seed
{
    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1
    }


    public interface IAsyncRepository<T> where T : BaseAuditModel
    {
        ResponseDTO GetResponseDTO(ResponseStatus status, Object oData, string Message);
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T entity);
        Task AttachAsync(T entity);
        Task<int> CountAsync(ISpecification<T> criteria);
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task DeleteAsync(Expression<Func<T, bool>> criteria);
        Task DeleteAsync(T entity);
        Task DeleteAsync(ISpecification<T> criteria);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> FindAsync(ISpecification<T> criteria);
        Task<T> FindOneAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindOneAsync(ISpecification<T> criteria);
        Task<T> FirstAsync(ISpecification<T> criteria);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);
        Task<IEnumerable<T>> GetAsync<TOrderBy>(Expression<Func<T, bool>> criteria, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);
        Task<IEnumerable<T>> GetAsync<TOrderBy>(ISpecification<T> specification, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByKeyAsync(object keyValue);
        Task<IQueryable<T>> GetQueryAsync(ISpecification<T> criteria);
        Task<IQueryable<T>> GetQueryAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetQueryAsync();
        Task<T> SingleAsync(Expression<Func<T, bool>> criteria);
        Task<T> SingleAsync(ISpecification<T> criteria);
        Task UpdateAsync(T entity);
        Task<T> GetByGuidAsync(Guid guid);
        public Task<T> FindOneAsyncForDelete(Expression<Func<T, bool>> criteria);

    }

}
