using E_Commerce.Model;
using E_Commerce.Repository;
using E_Commerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly loginRepository _loginRepository;
        private readonly JWTService _jwtService;
        public LoginController(loginRepository loginRepository,JWTService jWTService)
        {
            _loginRepository = loginRepository;
            _jwtService = jWTService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(loginModel login)
        {
            bool isValid = await _loginRepository.VerifySignIn(login);
            if (isValid)
            {
                var token = _jwtService.CreateToken();
                return Ok(new { Token = token ,message="Login Successful"});

            }
            else
            {
                return BadRequest("invaild credentials");
            }
        }

        

    }
}
