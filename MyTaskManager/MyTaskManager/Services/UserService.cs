using Microsoft.EntityFrameworkCore;
using MyTaskManager.Data;
using MyTaskManager.Models;

namespace MyTaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContext, ITokenService tokenService)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _tokenService = tokenService;
        }
        public User? ValidateCredentials(string username, string password)
        {
            //return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password); requisição válida
            if (username == "testuser" && password == "testpassword")
                return new User() { Id = 1, Username = "testuser", Password = "testpassword" };
            else
                return null;
        }

        public string GetUserIdFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token não encontrado.");
            }

            var user = _tokenService.ValidateToken(token);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Token Inválido.");
            }

            return user.Id.ToString();
        }
    }
}
