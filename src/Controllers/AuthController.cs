using CerealAPI.src.Models;
using CerealAPI.src.Models.DTOs;
using CerealAPI.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CerealAPI.src.Controllers
{
    /// <summary>
    /// Authentication controller for user registration and login. 
    /// TODO: make the registration reguire authentication and let the seed have an admin account ready.  
    /// </summary>
    /// <param name="authService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {        

        /// <summary>
        /// Register endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>>Register(UserDTO request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(user);
        }


        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var token = await authService.LoginAsync(request);
            if (token == null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(token);
        }      
    }
}
