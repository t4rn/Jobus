using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobus.DataAccess.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAllQueryable();

        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds TEntity to db. Returns the number of objects written to the underlying database.
        /// </summary>
        Task<int> AddAsync(TEntity entity);

        Task<int> UpdateAsync(TEntity entity);

        int Update(TEntity entity);

        Task<int> DeleteAsync(int id);
    }
}
