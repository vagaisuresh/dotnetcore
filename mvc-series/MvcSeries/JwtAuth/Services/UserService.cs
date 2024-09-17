using JwtAuth.Data;
using JwtAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Username == username && user.Password == password);

            if (user == null)
                throw new InvalidOperationException();

            return user;
        }


        public async Task<User> AuthenticateAsync2(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Username == username && user.Password == password) ?? throw new ArgumentException();
        }
    }
}