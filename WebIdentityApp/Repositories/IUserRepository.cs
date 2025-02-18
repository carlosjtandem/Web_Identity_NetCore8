using WebIdentityApp.Models;

namespace WebIdentityApp.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<bool> ConfirmEmailAsync(string userId);
        Task<List<User>> GetUsers();
    }
}
