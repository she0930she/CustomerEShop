using ApplicationCore.Entities;

namespace ApplicationCore.Repository;

public interface IUserRepoAsync: IRepositoryAsync<User>
{
    User SearchByEmailAndPassword(string email, string password);
    User SearchByEmail(string email);
}

