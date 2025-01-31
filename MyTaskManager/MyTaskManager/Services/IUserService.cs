using MyTaskManager.Models;

namespace MyTaskManager.Services
{
    public interface IUserService
    {
        User? ValidateCredentials(string username, string password);
        string GetUserIdFromToken();
    }
}
