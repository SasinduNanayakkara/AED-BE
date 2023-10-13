using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;
using AED_BE.DTO;
using AED_BE.DTO.RequestDto;

namespace AED_BE.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
      
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
        
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] LoginRequest loginRequest)
        {
            IActionResult response = Unauthorized();
            String token = await _loginService.Login(loginRequest);
            if (token != null) {
                response = Ok(new { access_token = token });
            }
            return response;

        }

      

    }
}
