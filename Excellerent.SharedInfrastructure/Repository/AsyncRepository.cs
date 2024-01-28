using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.SharedInfrastructure.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseAuditModel
    {
        private readonly EPPContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AsyncRepository(EPPContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }

        public ResponseDTO GetResponseDTO(ResponseStatus status, Object oData, string Message)
        {

            return new ResponseDTO()
            {
                Message = Message,
                Data = oData,
                ResponseStatus = status
            };
        }




        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(this._context);
                }
                return unitOfWork;
            }
            set
            {
                unitOfWork = new UnitOfWork(this._context);
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null || _context == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task AttachAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
        }

        public async Task<int> CountAsync(ISpecification<TEntity> criteria)
        {
            return await criteria.SatisfyingEntitiesFrom(await GetQueryAsync()).CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await (await GetQueryAsync()).CountAsync(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await (await GetQueryAsync()).CountAsync();
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> criteria)
        {
            IEnumerable<TEntity> records = await FindAsync(criteria);

            foreach (TEntity record in records)
            {
                await DeleteAsync(record);
            }
        }

        public async Task DeleteAsync(ISpecification<TEntity> criteria)
        {
            IEnumerable<TEntity> records = await FindAsync(criteria);

            foreach (TEntity record in records)
            {
                await DeleteAsync(record);
            }
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return (await GetQueryAsync()).Where(criteria);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(await GetQueryAsync()).AsEnumerable();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return await (await GetQueryAsync()).Where(criteria).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindOneAsyncForDelete(Expression<Func<TEntity, bool>> criteria)
        {
            return await (await GetQueryAsync()).Where(criteria).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindOneAsync(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntityFrom(await GetQueryAsync());
        }

        public async Task<TEntity> FirstAsync(ISpecification<TEntity> criteria)
        {
            return await criteria.SatisfyingEntitiesFrom(await GetQueryAsync()).FirstAsync();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await (await GetQueryAsync()).FirstAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return (await GetQueryAsync()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return (await GetQueryAsync()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return (await GetQueryAsync(criteria)).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return (await GetQueryAsync(criteria)).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return specification.SatisfyingEntitiesFrom(await GetQueryAsync()).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return specification.SatisfyingEntitiesFrom(await GetQueryAsync()).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return (await GetQueryAsync()).AsEnumerable();
        }

        public async Task<TEntity> GetByKeyAsync(object keyValue)
        {
            //EntityKey key = GetEntityKey<TEntity>(keyValue);

            //object originalItem;
            //if (((IObjectContextAdapter)DbContext).ObjectContext.TryGetObjectByKey(key, out originalItem))
            //{
            //    return (TEntity)originalItem;
            //}
            return default;
        }

        public async Task<IQueryable<TEntity>> GetQueryAsync(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntitiesFrom(await GetQueryAsync());
        }

        public async Task<IQueryable<TEntity>> GetQueryAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return (await GetQueryAsync()).Where(predicate);
        }

        public async Task<IQueryable<TEntity>> GetQueryAsync()
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            return query;
        }
        public async Task<TEntity> GetByGuidAsync(Guid guid)
        {
            return await DbContext.Set<TEntity>().FindAsync(guid);
        }
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return (await GetQueryAsync()).Single(criteria);
        }

        public async Task<TEntity> SingleAsync(ISpecification<TEntity> criteria)
        {
            return criteria.SatisfyingEntityFrom(await GetQueryAsync());
        }

        public async Task UpdateAsync(TEntity entity)
        {

            var keyName = DbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            var keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);

            var attachedObject = DbContext.ChangeTracker
                .Entries().FirstOrDefault(x => x.Metadata.FindPrimaryKey().Properties.First(y => y.Name == keyName) == keyValue);
            if (attachedObject != null)
            {
                attachedObject.State = EntityState.Detached;
            }


            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
            }

        #region private 
        public virtual int GetKey<T>(T entity)
        {
            var keyName = DbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }



        private DbContext DbContext
        {
            get
            {
                // if (this._context == null)
                // {
                //     if (this._connectionStringName == string.Empty)
                //         this._context = DbContextManager.Current;
                //     else
                //         this._context = DbContextManager.CurrentFor(this._connectionStringName);
                // }
                return this._context;
            }
        }
        private IUnitOfWork unitOfWork;
        #endregion
    }
}
