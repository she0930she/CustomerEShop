using System.Linq.Expressions;

namespace ApplicationCore.Repository;

public interface IRepositoryAsync<T> where T: class
{
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> Filter(Expression<Func<T,bool>> filter);
    Task<T> FilterByModel(Expression<Func<T,bool>> filter);
}