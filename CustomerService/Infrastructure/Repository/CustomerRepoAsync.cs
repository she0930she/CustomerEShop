using ApplicationCore.Entities;
using ApplicationCore.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class CustomerRepoAsync: BaseRepositoryAsync<Customer>, ICustomerRepoAsync
{
    public CustomerRepoAsync(CustomerDbContext customerDbContext) : base(customerDbContext)
    {
    }
    
}