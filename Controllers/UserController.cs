using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresenterAPI.Services;

namespace PresenterAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
