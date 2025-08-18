using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresenterAPI.DTOs;
using PresenterAPI.Services;

namespace PresenterAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService userService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await userService.RegisterUserAsync(request);
                if (user == null)
                {
                    return BadRequest("User registration failed.");
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                    Details = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await userService.LoginUserAsync(request);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ex.Message,
                    Details = ex.InnerException?.Message
                });
            }
        }
    }
}
