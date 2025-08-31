using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.DAL.EntityFrameworkCore.Context;

namespace OrderManagementSystem.DAL.Generics.DAL
{
    public class GenericDataAccesssLayerBuss<TEntity> : IGenericDataAccessLayerBuss<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public GenericDataAccesssLayerBuss(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }
        #region GETs
        public virtual async Task<TEntity> GetByIdAsync(
            object id,
            bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e =>
                EF.Property<object>(e, "Id").Equals(id));
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
        #endregion

        #region CREATEs
        public virtual async Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _dbSet.AddAsync(entity);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }

            return entity;
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            ArgumentNullException.ThrowIfNull(entities);

            await _dbSet.AddRangeAsync(entities);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }
        #endregion

        #region UPDATEs
        public virtual async Task UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }
        #endregion

        #region DELETEs
        public virtual async Task DeleteAsync(object id, bool saveChanges = true)
        {
            var entity = await GetByIdAsync(id, trackChanges: true);

            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, saveChanges);
        }
        public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);
            _dbSet.RemoveRange(entities);
        }
        #endregion

        #region QUERIES
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }
        #endregion


        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
