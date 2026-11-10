namespace RaceDay.Web.Models;
public enum UserRole { Organiser, Participant }
public record UserModel(int Id,string FullName,string Email,UserRole Role,string? Phone,string? ProfileImageUrl);
public record LoginModel(string Email,string Password);
public record RegisterModel(string FullName,string Email,string Password,UserRole Role,string? Phone);
public record EventModel(int Id,string Name,string Description,DateTime EventDate,string Location,decimal DistanceKm,string EventType,string? BannerImageUrl,int OrganiserId);
public record CategoryModel(int Id,int EventId,string Name,int? MinimumAge,int? MaximumAge,decimal? DistanceKm);
public record EnrollmentModel(int Id,int EventId,int ParticipantId,int CategoryId,TimeSpan? FinishTime,int? FinishingPosition,string? EventName,string? CategoryName,string? ParticipantName);
public record ResultModel(TimeSpan FinishTime,int FinishingPosition);
public class EventForm { public string Name{get;set;}=""; public string Description{get;set;}=""; public DateTime EventDate{get;set;}=DateTime.Today.AddDays(30); public string Location{get;set;}=""; public decimal DistanceKm{get;set;} public string EventType{get;set;}="Run"; public IFormFile? Banner{get;set;} }
public class CategoryForm { public string Name{get;set;}=""; public int? MinimumAge{get;set;} public int? MaximumAge{get;set;} public decimal? DistanceKm{get;set;} }
public class ProfileForm { public string FullName{get;set;}=""; public string? Phone{get;set;} public IFormFile? Picture{get;set;} }
