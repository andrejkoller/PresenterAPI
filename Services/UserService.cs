using Microsoft.EntityFrameworkCore;
using PresenterAPI.DTOs;
using PresenterAPI.Mappers;
using PresenterAPI.Models;

namespace PresenterAPI.Services
{
    public class UserService(PresenterDbContext dbContext)
    {
        public async Task<PublicUser?> RegisterUserAsync(RegisterRequest request)
        {
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return UserMapper.MapToPublicUser(user);
        }
    }
}
