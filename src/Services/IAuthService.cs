using CerealAPI.src.Models;
using CerealAPI.src.Models.DTOs;

namespace CerealAPI.src.Services
{
    /// <summary>
    /// Interface for authentication service 
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Register user to database
        /// </summary>
        /// <param name="request"></param>
        /// <returns>User or null if username is already taken</returns>
        Task<User> RegisterAsync(UserDTO request);

        /// <summary>
        /// Login to user 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Token</returns>
        Task<string> LoginAsync(UserDTO request);

    }
}
