using Hangfire;
using Microsoft.EntityFrameworkCore;
using OMSWeb.Data.Access.DAL;

namespace OMSWeb.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly string cacheKey = $"{typeof(T)}";
		private readonly dbcontext _dbContext;
		public GenericRepository(dbcontext dbContext)
		{
			_dbContext = dbContext;
		}
		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			IReadOnlyList<T> cachedList;
				cachedList = await _dbContext.Set<T>().ToListAsync();
			return cachedList;
		}
		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			BackgroundJob.Enqueue(() => RefreshCache());
			return entity;
		}
		public async Task UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
			BackgroundJob.Enqueue(() => RefreshCache());
		}
		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
			BackgroundJob.Enqueue(() => RefreshCache());
		}
		public async Task RefreshCache()
		{
			var cachedList = await _dbContext.Set<T>().ToListAsync();
		}
	}
}
