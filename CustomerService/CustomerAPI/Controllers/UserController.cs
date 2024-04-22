//using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Services;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace CustomerAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IUserServiceAsync _userServiceAsync;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public UserController(IUserServiceAsync userServiceAsync, JwtTokenHandler jwtTokenHandler)
    {
        _userServiceAsync = userServiceAsync;
        _jwtTokenHandler = jwtTokenHandler;
    }
    

    [HttpGet]
    [Route("getAllUsers")]
    [Authorize]
    public async Task< IActionResult> GetAllUsers()
    {
        return Ok( await _userServiceAsync.GetAllUsers());
    }
    

    
    [HttpPost]
    [Route("createNewUser")]
    public async Task<IActionResult> CreateNewUser(UserRequestModel usrReq)
    {
        if (usrReq == null) return BadRequest(new { message = "please enter required fields" });
        
        return Ok( await _userServiceAsync.CreateNewUser(usrReq));
    }
    
    [HttpPut]
    [Route("updateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UserUpdateModel usrReq)
    {
        if (usrReq == null) return BadRequest(new { message = "please enter required fields" });
        return Ok( await _userServiceAsync.UpdateUserAsync(usrReq));
    }
    
    
    [HttpDelete]
    [Route("deleteUserByEmail")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(EmailRequest req)
    {
        if (req == null) return BadRequest();
        return Ok( await _userServiceAsync.DeleteUserAsync(req));
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
    
    
    // [HttpGet]
    // [Route("getUserById")]
    // public async Task< IActionResult> GetUser(int id)
    // {
    //     return Ok( await _userServiceAsync.GetByUserIdAsync(id));
    // }
    
}