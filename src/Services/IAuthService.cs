using CerealAPI.src.Models;
using CerealAPI.src.Models.DTOs;

namespace CerealAPI.src.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserDTO request);
        Task<string> LoginAsync(UserDTO request);

    }
}
