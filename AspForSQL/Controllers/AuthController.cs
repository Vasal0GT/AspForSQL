using AspForSQL.Enteties;
using AspForSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspForSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new ();
        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Pasword);
            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> login(UserDto request)
        {
            if (user.UserName != request.UserName)
            {
                return BadRequest("User not Found");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Pasword)
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Password not found");
            }

            string token = "success";
            return Ok(token);
        }
    }
}
