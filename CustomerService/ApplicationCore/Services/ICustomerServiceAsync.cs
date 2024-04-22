using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;

namespace ApplicationCore.Services;

public interface ICustomerServiceAsync
{
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<IEnumerable<CustomerResponseModel>> GetAllCustomerModel();
    Task<int> AddNewCustomerModel(CustomerRequestModel crm);
    Task<int> AddNewCustomer(Customer c);
    Task<int> GetCustomerCountAsync();
    Task<IEnumerable<CustomerResponseModel>> GetCustomerModelByName(string name);
}