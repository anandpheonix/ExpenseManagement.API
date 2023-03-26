using DataAccess.Models;

namespace Application.Auth;

public interface ITokenHandler
{
    Task<string> GenerateToken(Users users);
}
