using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController: ControllerBase
{
    private readonly ICustomerServiceAsync _customerServiceAync;

    public CustomerController(ICustomerServiceAsync customerServiceAync)
    {
        _customerServiceAync = customerServiceAync;
    }


    // [HttpGet]
    // public async Task<IActionResult> GetAllCustomerModel()
    // {
    //     return Ok(await _customerServiceAync.GetAllCustomerModel());
    // }
    
    [HttpGet]
    public async Task< IActionResult> GetAllCustomers()
    {
        return Ok( await _customerServiceAync.GetAllCustomers());
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewCustomer(Customer obj)
    {
        
        return Ok( await _customerServiceAync.AddNewCustomer(obj));
    }
    
    
}