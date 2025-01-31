using MyTaskManager.Models;

namespace MyTaskManager.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        User ValidateToken(string token);
    }
}
