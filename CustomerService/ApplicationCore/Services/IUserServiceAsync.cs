using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;

namespace ApplicationCore.Services;

public interface IUserServiceAsync
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<int> CreateNewUser(UserRequestModel usrRequestModel);
    Task<int> CreateNewAdmin(UserRequestModel usrRequestModel);
    Task<int> UpdateUserAsync(UserUpdateModel usrReq);
    Task<int> UpdateAdminAsync(UserUpdateModel usrReq);
    Task<int> DeleteUserAsync(EmailRequest req);
    Task<User> GetByUserIdAsync(int id);
    User GetUserByEmailAndPassword(string email, string password);
    User GetMyUser(EmailRequest req);


}