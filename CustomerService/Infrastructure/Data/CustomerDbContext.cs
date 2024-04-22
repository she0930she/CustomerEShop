using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CustomerDbContext: DbContext
{
    public CustomerDbContext (DbContextOptions<CustomerDbContext> options):base(options)
    {

    }
    // set table name
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
}