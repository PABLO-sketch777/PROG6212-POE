using Microsoft.EntityFrameworkCore; using RaceDay.Api.Models;
namespace RaceDay.Api.Data;
public class RaceDayDbContext(DbContextOptions<RaceDayDbContext> options):DbContext(options){
 public DbSet<User> Users=>Set<User>(); public DbSet<Event> Events=>Set<Event>(); public DbSet<Category> Categories=>Set<Category>(); public DbSet<Enrollment> Enrollments=>Set<Enrollment>(); public DbSet<Result> Results=>Set<Result>();
 protected override void OnModelCreating(ModelBuilder b){ b.Entity<User>().HasIndex(x=>x.Email).IsUnique(); b.Entity<User>().Property(x=>x.Role).HasConversion<string>(); b.Entity<Event>().Property(x=>x.EventType).HasConversion<string>(); b.Entity<Event>().HasOne(x=>x.Organiser).WithMany(x=>x.OrganisedEvents).HasForeignKey(x=>x.OrganiserId).OnDelete(DeleteBehavior.Restrict); b.Entity<Enrollment>().HasIndex(x=>new{x.EventId,x.ParticipantId}).IsUnique(); b.Entity<Enrollment>().HasOne(x=>x.Participant).WithMany(x=>x.Enrollments).HasForeignKey(x=>x.ParticipantId).OnDelete(DeleteBehavior.Restrict); b.Entity<Result>().HasOne(x=>x.Enrollment).WithOne(x=>x.Result).HasForeignKey<Result>(x=>x.EnrollmentId); }
}
