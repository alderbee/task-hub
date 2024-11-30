using Microsoft.EntityFrameworkCore;
using TaskPulse.Domain.Entities;

namespace TaskPulse.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UsersData> UsersData { get; set; }
    
    
    public DbSet<TaskModel> Task { get; set; }
}