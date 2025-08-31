using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.DAL.Generics.DAL
{
    public interface IGenericDataAccessLayerBuss<TEntity> where TEntity : class
    {
        #region GETs
        Task<TEntity> GetByIdAsync(object id,
            bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IReadOnlyList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool trackChanges = false,
            params Expression<Func<TEntity, object>>[] includeProperties);
        #endregion

        #region CREATEs
        Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true);
        #endregion

        #region UPDATEs
        Task UpdateAsync(TEntity entity, bool saveChanges = true);
        void UpdateRange(IEnumerable<TEntity> entities);
        #endregion

        #region DELETEs
        Task DeleteAsync(object id, bool saveChanges = true);
        Task DeleteAsync(TEntity entity, bool saveChanges = true);
        void DeleteRange(IEnumerable<TEntity> entities);
        #endregion

        Task<int> SaveChangesAsync();

        #region QUERIES
        IQueryable<TEntity> GetQueryable();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion
    }
}
