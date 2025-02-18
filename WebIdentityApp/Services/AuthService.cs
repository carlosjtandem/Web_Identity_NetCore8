using System.Security.Cryptography;
using System.Text;
using WebIdentityApp.Models;
using WebIdentityApp.Repositories;

namespace WebIdentityApp.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            var hashedBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            var user = new User { Email = email, PasswordHash = HashPassword(password) };
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.PasswordHash != HashPassword(password))
            {
                return null;
            }
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {

            var user = new User {  };
            return await _userRepository.GetUsers();
        }
        internal async Task<bool> ConfirmEmailAsync(string userId)
        {


            return true;
        }
    }
}
