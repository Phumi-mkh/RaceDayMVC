using Microsoft.EntityFrameworkCore;
using RaceBay.API.Models;

namespace RaceBay.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Result> Results { get; set; }
}
