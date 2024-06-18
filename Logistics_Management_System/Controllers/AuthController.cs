using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logistics_Management_System.Models;
using Logistics_Management_System.Respository;
using Logistics_Management_System.Models.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logistics_Management_System.Service;

namespace Logistics_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogistics _logistics;
        private readonly TokenService _tokenService;

        public AuthController(ILogistics logistics, TokenService tokenService)
        {
            _logistics = logistics;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> ValidateUser(User user)
        {
            var result = await _logistics.Login(user);
            if (result.Message == "Login unsuccessful")
            {
                return Unauthorized(result);
            }
            var token = _tokenService.GenerateJwtToken(result);
            return Ok(new
            {
                token,
                result.UserId,
                result.Role,
                result.Message
            });
        }
    }
}
