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
    public class AuthService(PresenterDbContext dbContext)
    {
        public async Task<PublicUser> RegisterUserAsync(RegisterRequest request)
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

        public async Task<AuthResponse> LoginUserAsync(LoginRequest request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            user.LastLogin = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            string jwtToken = GenerateJwtToken(user);

            return new AuthResponse
            {
                User = UserMapper.MapToPublicUser(user),
                Token = jwtToken
            };
        }

        public string GenerateJwtToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email)
                };

            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new InvalidOperationException("JWT_SECRET environment variable is not set.");

            if (jwtSecret.Length < 32)
                throw new InvalidOperationException("JWT_SECRET is too short. Use at least 32 characters.");

            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var expireDays = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRE_DAYS"), out var days) ? days : 1;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireDays),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
