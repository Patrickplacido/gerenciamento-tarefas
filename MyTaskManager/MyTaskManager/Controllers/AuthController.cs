using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Models;
using MyTaskManager.Services;

namespace MyTaskManager.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userService.ValidateCredentials(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(new { message = "Login bem-sucedido" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok();
        }
    }
    
}
