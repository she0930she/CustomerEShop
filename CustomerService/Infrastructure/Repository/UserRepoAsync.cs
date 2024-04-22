using ApplicationCore.Entities;
using ApplicationCore.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepoAsync: BaseRepositoryAsync<User>, IUserRepoAsync
{
    private readonly CustomerDbContext _customerDbContext;
    
    public UserRepoAsync(CustomerDbContext customerDbContext) : base(customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }

    public  User SearchByEmailAndPassword(string email, string password)
    {
        var res =  _customerDbContext.Users.Where(
            item => item.Email == email && item.Password == password
        ).FirstOrDefault();
        return res;
    }

    public User SearchByEmail(string email)
    {
        var res =  _customerDbContext.Users.Where(
            item => item.Email == email
        ).FirstOrDefault();
        return res;
    }
}