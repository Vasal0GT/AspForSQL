using AspForSQL.Enteties;
using AspForSQL.Models;

namespace AspForSQL.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDTO?> LoginAsync(UserDto request);
        Task<TokenResponseDTO?> RefreshTokenAsync(RefreshRequestTokenDTO request);
    }
}
