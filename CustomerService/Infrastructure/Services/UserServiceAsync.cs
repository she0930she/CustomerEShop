using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Model;
using ApplicationCore.Model.Request;
using ApplicationCore.Repository;
using ApplicationCore.Services;

namespace Infrastructure.Services;

public class UserServiceAsync: IUserServiceAsync
{
    private readonly IUserRepoAsync _userRepoAsync;

    public UserServiceAsync(IUserRepoAsync userRepoAsync)
    {
        _userRepoAsync = userRepoAsync;
    }


    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepoAsync.GetAllAsync();
    }

    public async Task<int> CreateNewUser(UserRequestModel usrRequestModel)
    {
        //if (usrRequestModel == null) return;
        // assign a Role to User entity
        User usr = new User()
        {
            FullName = usrRequestModel.FullName,
            Email = usrRequestModel.Email,
            Password = usrRequestModel.Password,
            Role = "Customer",
        };
        return await _userRepoAsync.InsertAsync(usr);
    }

    public async Task<int> CreateNewAdmin(UserRequestModel usrRequestModel)
    {
        // assign a Admin Role to User entity
        User usr = new User()
        {
            FullName = usrRequestModel.FullName,
            Email = usrRequestModel.Email,
            Password = usrRequestModel.Password,
            Role = "Admin",
        };
        return await _userRepoAsync.InsertAsync(usr);
    }

    public async Task<int> UpdateUserAsync(UserUpdateModel usrReq)
    {
        User usr = _userRepoAsync.SearchByEmail(usrReq.Email);
        
        // assign a customer Role to User entity
        User resUser = new User()
        {
            Id = usr.Id,
            FullName = usrReq.FullName,
            Email = usrReq.Email,
            Password = usrReq.Password,
            Role = "Customer",
        };
        return await _userRepoAsync.UpdateAsync(resUser);
    }
    
    public async Task<int> UpdateAdminAsync(UserUpdateModel usrReq)
    {
        User usr = _userRepoAsync.SearchByEmail(usrReq.Email);
        
        // assign a Admin Role to User entity
        User resUser = new User()
        {
            Id = usr.Id,
            FullName = usrReq.FullName,
            Email = usrReq.Email,
            Password = usrReq.Password,
            Role = "Admin",
        };
        return await _userRepoAsync.UpdateAsync(resUser);
    }

    public async Task<int> DeleteUserAsync(EmailRequest req)
    {
        var usr =  _userRepoAsync.SearchByEmail(req.Email);
        
        return await _userRepoAsync.DeleteAsync(usr.Id);
    }

    public async Task<User> GetByUserIdAsync(int id)
    {
        return await _userRepoAsync.GetByIdAsync(id);
    }

    public  User GetUserByEmailAndPassword(string email, string password)
    {
        var usr =  _userRepoAsync.SearchByEmailAndPassword(email, password);
        
        return usr;
    }

    public User GetMyUser(EmailRequest req)
    {
        var usr = _userRepoAsync.SearchByEmail(req.Email);
        return usr;
    }
}