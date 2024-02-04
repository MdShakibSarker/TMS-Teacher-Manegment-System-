using Microsoft.EntityFrameworkCore;

namespace TMS.Models;

public class TeacherDbContext:DbContext
{
    public TeacherDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Teacher> Teachers { get; set; }
}
