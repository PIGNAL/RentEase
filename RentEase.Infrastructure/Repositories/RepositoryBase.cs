using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain.Common;
using RentEase.Infrastructure.Persistence;

namespace RentEase.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseDomainModel
    {
        protected readonly RentEaseDbContext _context;

        public RepositoryBase(RentEaseDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(x => !x.IsDeleted).Where(predicate);
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
