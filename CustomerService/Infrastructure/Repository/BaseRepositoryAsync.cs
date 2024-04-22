using System.Linq.Expressions;
using ApplicationCore.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BaseRepositoryAsync<T>: IRepositoryAsync<T> where T : class
{
    private readonly CustomerDbContext _customerDbContext;

    public BaseRepositoryAsync(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }




    public Task<int> InsertAsync(T entity)
    {
        _customerDbContext.Set<T>().AddAsync(entity);
        return _customerDbContext.SaveChangesAsync();
    }

    public Task<int> UpdateAsync(T entity)
    {
        _customerDbContext.Entry(entity).State = EntityState.Modified;
        return _customerDbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _customerDbContext.Remove(entity);
        return await _customerDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _customerDbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var res = await _customerDbContext.Set<T>().FindAsync(id);
        return res;
    }



    public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> filter)
    {
        return await _customerDbContext.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<T> FilterByModel(Expression<Func<T, bool>> filter)
    {

        return await _customerDbContext.Set<T>().Where(filter).FirstOrDefaultAsync();
    }
    

    
}