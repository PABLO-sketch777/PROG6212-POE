using Microsoft.AspNetCore.Mvc; using RaceDay.Api.Data; using RaceDay.Api.DTOs; using RaceDay.Api.Services;
namespace RaceDay.Api.Controllers;
[ApiController,Route("api/profile")]
public class ProfileController(RaceDayDbContext db,SessionAuth auth):ControllerBase{
 [HttpGet] public ActionResult<UserResponse> Get(){var u=auth.Current;return u is null?Unauthorized():Ok(SessionAuth.View(u));}
 [HttpPut] public async Task<ActionResult<UserResponse>> Put(UpdateProfileRequest r){var u=auth.Current;if(u is null)return Unauthorized();u.FullName=r.FullName;u.Phone=r.Phone;await db.SaveChangesAsync();return Ok(SessionAuth.View(u));}
}
