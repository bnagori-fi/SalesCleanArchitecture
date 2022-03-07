using Microsoft.EntityFrameworkCore;
using Sales.Application.Contracts.Persistence;
using Sales.Domain.Common;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T, TKey> : IAsyncRepository<T, TKey> where T : EntityBase<TKey>
    {
        protected readonly SalesDbContext dbContext;

        public RepositoryBase(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbContext.Set<T>().Where(predicate).ToListAsync();
        }


        public async Task<T> GetByIdAsync(TKey id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
