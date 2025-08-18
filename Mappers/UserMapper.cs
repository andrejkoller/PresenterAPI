using PresenterAPI.DTOs;
using PresenterAPI.Models;

namespace PresenterAPI.Mappers
{
    public static class UserMapper
    {
        public static PublicUser MapToPublicUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            return new PublicUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Theme = user.Theme
            };
        }
    }
}
