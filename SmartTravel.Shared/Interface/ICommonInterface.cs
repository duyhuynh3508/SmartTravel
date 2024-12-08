using System.Linq.Expressions;
using SmartTravel.Shared.ResponseExtension;

namespace SmartTravel.Shared.Interface
{
    public interface ICommonInterface<T> where T : class
    {
        Task<Response> CreateAsync(T entity);
        Task<Response> UpdateAsync(T entity);
        Task<Response> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids);
        Task<T> GetByNameAsync(string name);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
