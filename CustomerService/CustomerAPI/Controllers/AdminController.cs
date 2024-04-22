using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Services;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController: ControllerBase
{
    private readonly IUserServiceAsync _userServiceAsync;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public AdminController(IUserServiceAsync userServiceAsync, JwtTokenHandler jwtTokenHandler)
    {
        _userServiceAsync = userServiceAsync;
        _jwtTokenHandler = jwtTokenHandler;
    }


    // create new admin
    [HttpPost]
    [Route("createNewAdmin")]
    public async Task<IActionResult> AddNewAdmin(UserRequestModel userReq)
    {
        if (userReq == null) return BadRequest(new { message = "please enter required fields" });
        
        return Ok( await _userServiceAsync.CreateNewAdmin(userReq));
    }
    
    // update admin
    [HttpPut]
    [Route("updateAdmin")]
    [Authorize]
    public async Task<IActionResult> UpdateAdmin(UserUpdateModel userReq)
    {
        if (userReq == null) return BadRequest(new { message = "please enter required fields" });
        return Ok( await _userServiceAsync.UpdateAdminAsync(userReq));
    }
    
    // delete Admin
    [HttpDelete]
    [Route("deleteAdminByEmail")]
    [Authorize]
    public async Task<IActionResult> DeleteAdmin(EmailRequest req)
    {
        if (req == null) return BadRequest();
        return Ok( await _userServiceAsync.DeleteUserAsync(req));
    }
    
    [HttpGet]
    [Route("getMyUser")]
    //[Authorize]
    public   IActionResult GetMyUser(EmailRequest req)
    {
        if (req == null) return BadRequest();
        return Ok( _userServiceAsync.GetMyUser(req));
    }
    
    
    // below are user related
    
    [HttpGet]
    [Route("getAllUsers")]
    [Authorize]
    public async Task< IActionResult> GetAllUsers()
    {
        return Ok( await _userServiceAsync.GetAllUsers());
    }
    
    // [HttpGet]
    // [Route("getUserById")]
    // public async Task< IActionResult> GetUser(int id)
    // {
    //     return Ok( await _userServiceAsync.GetByUserIdAsync(id));
    // }
    
    [HttpPut]
    [Route("updateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UserUpdateModel userReq)
    {
        if (userReq == null) return BadRequest(new { message = "please enter required fields" });
        return Ok( await _userServiceAsync.UpdateUserAsync(userReq));
    }
    
    [HttpDelete]
    [Route("deleteUserByEmail")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(EmailRequest req)
    {
        if (req == null) return BadRequest();
        return Ok( await _userServiceAsync.DeleteUserAsync(req));
    }
    
    
    
    // below are for test use, should not be placed in production
    
    [HttpGet]
    [Route("getAllUsersTest")]
    public async Task< IActionResult> GetAllUsersTest()
    {
        return Ok( await _userServiceAsync.GetAllUsers());
    }
    
    [HttpPost]
    [Route("login")]
    public  IActionResult Login(AuthenticationRequest usrReq)
    {
        var usr = _userServiceAsync.GetUserByEmailAndPassword(usrReq.Email, usrReq.Password);
        if (usr == null) return Unauthorized();
        var authRes = _jwtTokenHandler.GenerateToken( usr);
        return Ok( authRes);
    }
    
}