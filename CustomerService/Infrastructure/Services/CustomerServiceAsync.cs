using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using ApplicationCore.Repository;
using ApplicationCore.Services;

namespace Infrastructure.Services;

public class CustomerServiceAsync: ICustomerServiceAsync
{
    private readonly ICustomerRepoAsync _customerRepoAsync;

    public CustomerServiceAsync(ICustomerRepoAsync customerRepoAsync)
    {
        _customerRepoAsync = customerRepoAsync;
    }


    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        return await _customerRepoAsync.GetAllAsync();
    }

    public async Task<int> AddNewCustomer(Customer obj)
    {
        return await _customerRepoAsync.InsertAsync(obj);
    }





    public async Task<IEnumerable<CustomerResponseModel>> GetAllCustomerModel()
    {
        var res = await _customerRepoAsync.GetAllAsync();
        List<CustomerResponseModel> lst = new List<CustomerResponseModel>();
        foreach (Customer c in res)
        {
            CustomerResponseModel crm = new CustomerResponseModel();
            crm.Name = c.Name;
            crm.Address = c.Address;
            crm.Email = c.Email;
            lst.Add(crm);
        }

        return lst;
    }

    public Task<int> AddNewCustomerModel(CustomerRequestModel crm)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCustomerCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerResponseModel>> GetCustomerModelByName(string name)
    {
        throw new NotImplementedException();
    }
}