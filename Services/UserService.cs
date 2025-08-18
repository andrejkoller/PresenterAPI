using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PresenterAPI.DTOs;
using PresenterAPI.Mappers;
using PresenterAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PresenterAPI.Services
{
    public class UserService(PresenterDbContext dbContext)
    {
        public async Task<PublicUser?> GetUserByEmailAsync(string email)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null ? UserMapper.MapToPublicUser(user) : null;
        }

        public async Task<PublicUser?> GetUserByIdAsync(int userId)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user != null ? UserMapper.MapToPublicUser(user) : null;
        }

        public async Task<List<PublicUser>> GetAllUsersAsync()
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .ToListAsync();
            return [.. users.Select(UserMapper.MapToPublicUser)];
        }
    }
}
