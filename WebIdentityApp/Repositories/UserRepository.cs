using Dapper;
using WebIdentityApp.Data;
using WebIdentityApp.Models;

namespace WebIdentityApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = _dapperContext.CreateDbConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = email });
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using var connection = _dapperContext.CreateDbConnection();
            //user.Id = Guid.NewGuid().ToString();
            Random rnd = new Random();
            user.Id= rnd.Next(1, 1000);
            var sql = "INSERT INTO Users (Id, Email, PasswordHash) VALUES (@Id, @Email, @PasswordHash)";
            await connection.ExecuteAsync(sql, user);
            return user;
        }

        public async Task<bool> ConfirmEmailAsync(string userId)
        {
            using var connection = _dapperContext.CreateDbConnection();
            var sql = "UPDATE Users SET EmailConfirmed = TRUE WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = userId });
            return rowsAffected > 0;
        }

        public async Task<List<User>> GetUsers()
        {
            using var connection = _dapperContext.CreateDbConnection();
            var sql = "SELECT * FROM users";
            var returned=await connection.QueryAsync<User>(sql);
            return returned.ToList();
        }
    }
}
