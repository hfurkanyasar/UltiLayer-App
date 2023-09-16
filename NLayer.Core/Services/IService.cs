using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIDAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        //queryable olması sorgunun kullanılırken orderby tolist vs. kullanımı olması için
        // taki enumarable komutu verilene kadar(tolistasync) bekler.
        IQueryable<T> Where(Expression<Func<T, bool>> expresion);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expresion);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);
        
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
