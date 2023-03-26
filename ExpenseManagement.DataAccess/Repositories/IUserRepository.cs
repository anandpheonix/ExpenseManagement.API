using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IUserRepository
{
    Task<Tuple<Users, string>> AuthenticateUser(string username, string password);
}
