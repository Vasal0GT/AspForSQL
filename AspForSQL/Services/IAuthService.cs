using AspForSQL.Enteties;
using AspForSQL.Models;

namespace AspForSQL.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request); 
    }
}
