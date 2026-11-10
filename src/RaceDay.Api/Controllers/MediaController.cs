using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using RaceDay.Api.Data; using RaceDay.Api.Models; using RaceDay.Api.Services;
namespace RaceDay.Api.Controllers;
[ApiController,Route("api")]
public class MediaController(RaceDayDbContext db,SessionAuth auth,IBlobStorageService blobs):ControllerBase{
 [HttpPost("events/{eventId:int}/banner"),RequestSizeLimit(8_000_000)] public async Task<IActionResult> EventBanner(int eventId,IFormFile file,CancellationToken token){var e=await db.Events.FindAsync([eventId],token);if(e is null)return NotFound();if(!auth.Is(UserRole.Organiser)||e.OrganiserId!=auth.Current!.Id)return Forbid();if(file.Length==0||!file.ContentType.StartsWith("image/"))return BadRequest("Only image files are accepted");e.BannerImageUrl=await blobs.UploadAsync(file,"events",token);await db.SaveChangesAsync(token);return Ok(new{url=e.BannerImageUrl});}
 [HttpPost("profile/image"),RequestSizeLimit(5_000_000)] public async Task<IActionResult> ProfileImage(IFormFile file,CancellationToken token){var u=auth.Current;if(u is null)return Unauthorized();if(file.Length==0||!file.ContentType.StartsWith("image/"))return BadRequest("Only image files are accepted");u.ProfileImageUrl=await blobs.UploadAsync(file,"profiles",token);await db.SaveChangesAsync(token);return Ok(new{url=u.ProfileImageUrl});}
}
