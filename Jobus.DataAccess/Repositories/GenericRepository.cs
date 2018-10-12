using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Jobus.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            //_dbContext.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
            //return await _dbContext.SaveChangesAsync();

            var entity = await GetByIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Update and save changes only for specific type [untested]
        /// </summary>
        public async Task<int> SaveChangesAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            var original = _dbContext.ChangeTracker.Entries()
                        .Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)
                        .GroupBy(x => x.State)
                        .ToList();

            foreach (var entry in _dbContext.ChangeTracker.Entries().Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType())))
            {
                entry.State = EntityState.Unchanged;
            }

            var rowsAffected = await _dbContext.SaveChangesAsync();

            foreach (var state in original)
            {
                foreach (var entry in state)
                {
                    entry.State = state.Key;
                }
            }

            return rowsAffected;
        }

        public async Task<int> SaveChangesAsyncSafety()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                DetachAllEntities();
                throw ex;
            }
        }

        private void DetachAllEntities() => _dbContext
            .ChangeTracker
            .Entries()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted)
            .ToList()
            .ForEach(entry => entry.State = EntityState.Detached);
    }

    public static class RepositoryExtensions
    {
        /// <summary>
        /// Gets the value of a column with a specified index as an instance of System.String or NULL, if it's a DBNull.Value
        /// </summary>
        public static string GetStringOrNull(this DbDataReader dr, int colIndex)
        {
            return !dr.IsDBNull(colIndex) ? dr.GetString(colIndex) : null;
        }

        /// <summary>
        /// Gets the value of a column with the specified name as an instance of System.String or NULL, if it's a DBNull.Value
        /// </summary>
        public static string GetStringOrNull(this DbDataReader dr, string fieldName)
        {
            int colIndex = dr.GetOrdinal(fieldName);
            return !dr.IsDBNull(colIndex) ? dr.GetString(colIndex) : null;
        }
    }
}
