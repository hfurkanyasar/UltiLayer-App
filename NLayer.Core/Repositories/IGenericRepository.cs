using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIDAsync(int id);

        //queryable olması sorgunun kullanılırken orderby tolist vs. kullanımı olması için
        // taki enumarable komutu verilene kadar(tolistasync).
        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool>> expresion);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expresion);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
