using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using RaceDay.Api.Data; using RaceDay.Api.DTOs; using RaceDay.Api.Services;
namespace RaceDay.Api.Controllers;
[ApiController,Route("api/auth")]
public class AuthController(RaceDayDbContext db,IPasswordService passwords):ControllerBase{
 [HttpPost("register")] public async Task<ActionResult<UserResponse>> Register(RegisterRequest r){if(await db.Users.AnyAsync(x=>x.Email==r.Email.ToLower()))return Conflict("Email already registered");if(!Enum.IsDefined(r.Role))return BadRequest("Invalid role");var u=new RaceDay.Api.Models.User{FullName=r.FullName,Email=r.Email.Trim().ToLower(),PasswordHash=passwords.Hash(r.Password),Role=r.Role,Phone=r.Phone};db.Users.Add(u);await db.SaveChangesAsync();HttpContext.Session.SetInt32("UserId",u.Id);return Ok(new UserResponse(u.Id,u.FullName,u.Email,u.Role,u.Phone,u.ProfileImageUrl));}
 [HttpPost("login")] public async Task<ActionResult<UserResponse>> Login(LoginRequest r){var u=await db.Users.SingleOrDefaultAsync(x=>x.Email==r.Email.ToLower());if(u is null||!passwords.Verify(r.Password,u.PasswordHash))return Unauthorized("Invalid credentials");HttpContext.Session.SetInt32("UserId",u.Id);return Ok(new UserResponse(u.Id,u.FullName,u.Email,u.Role,u.Phone,u.ProfileImageUrl));}
 [HttpPost("logout")] public IActionResult Logout(){HttpContext.Session.Clear();return NoContent();}
 [HttpGet("me")] public async Task<ActionResult<UserResponse>> Me(){var id=HttpContext.Session.GetInt32("UserId");var u=id is null?null:await db.Users.FindAsync(id);return u is null?Unauthorized():Ok(new UserResponse(u.Id,u.FullName,u.Email,u.Role,u.Phone,u.ProfileImageUrl));}
}
