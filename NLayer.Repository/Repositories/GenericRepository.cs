using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);

        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expresion)
        {
            return await _dbSet.AnyAsync(expresion);
        }

        public IQueryable<T> GetAll()
        {
            // çekmiş oldugu datayı memeorye almasın demek asnotracking.
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            // entitiyin stateini deleted olarak işaretlenir.
            // savchanges çağırıldığında gidip state deleted olanları bulur siler.
            //_context.Entry(entity).State = EntityState.Deleted;
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);

        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expresion)
        {
            return _dbSet.Where(expresion);
        }
    }
}
