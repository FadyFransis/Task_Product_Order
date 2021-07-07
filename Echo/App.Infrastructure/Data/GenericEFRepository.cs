using Microsoft.EntityFrameworkCore;
using App.Core.Interfaces.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Core.Entities.Base;
using System.Collections.Generic;
using App.Infrastructure.Extensions;

namespace App.Infrastructure.Data
{
    public class GenericEFRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext mContext;

        public GenericEFRepository(DbContext context)
        {
            mContext = context;
        }


        public virtual Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> predicate, string sort, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = mContext.Set<T>().Where(predicate).ApplySort(sort);
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return Task.FromResult(query);
        }

        public virtual Task<(IQueryable<T> Result, int TotalItems)> GetAll(Expression<Func<T, bool>> predicate, string sort, int? page = null, int? limit = null, params Expression<Func<T, object>>[] includeProperties)
        {
            Expression<Func<T, bool>> defaultpredicate = x => x.RecordStatus == RecordStatus.Enabled;
            IQueryable<T> query = null;
            if (predicate == null)
                query = mContext.Set<T>().Where(defaultpredicate).ApplySort(sort);
            else
            {
                query = mContext.Set<T>().Where(predicate).Where(defaultpredicate).ApplySort(sort);
            }
            var count = query.Count();
            if (page != null && limit != null)
            {
                query = query.Skip(page.Value * limit.Value);
                query = query.Take(limit.Value);
            }
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return Task.FromResult((query, count));
        }
        public virtual Task<IQueryable<T>> GetAllIncludeString(Expression<Func<T, bool>> predicate, string sort, params string[] includeProperties)
        {
            IQueryable<T> query = mContext.Set<T>().Where(predicate).ApplySort(sort);
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return Task.FromResult(query);
        }

        public virtual Task<(IQueryable<T> Result, int TotalItems)> GetAllIncludeString(Expression<Func<T, bool>> predicate, string sort, int? page = null, int? limit = null, params string[] includeProperties)
        {
            IQueryable<T> query = mContext.Set<T>().Where(predicate).ApplySort(sort);

            var count = query.Count();

            if (page != null && limit != null)
            {
                query = query.Skip(page.Value * limit.Value);
                query = query.Take(limit.Value);
            }

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return Task.FromResult((query, count));
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = mContext.Set<T>().Where(predicate).AsNoTracking();

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> FindSingleIncludeString(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            var query = mContext.Set<T>().Where(predicate);

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return await query.SingleOrDefaultAsync();
        }

        public virtual Task Add(T entity)
        {
            var result = mContext.Set<T>().AddAsync(entity);
            return result.AsTask();
        }
        public virtual Task AddMultipleEntities(IEnumerable<T> entities)
        {
            var result = mContext.Set<T>().AddRangeAsync(entities);
            return result;
        }
        public virtual void Delete(T entity)
        {
            mContext.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            entity.LastUpdatedDate = DateTime.Now;
            mContext.Entry(entity).State = EntityState.Modified;
        }


        public virtual void UpdateRange(List<T> entities)
        {
            mContext.Set<T>().UpdateRange(entities);
        }

        public virtual async Task<int> Save()
        {
            return await mContext.SaveChangesAsync();
        }

    }
}
