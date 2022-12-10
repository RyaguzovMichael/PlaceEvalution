using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DbMaps;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.DbContexts;

public class ExamDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<PlaceDbModel> Places { get; set; }
    public DbSet<ReviewDbModel> Reviews { get; set; }

    public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserDbMap());
        modelBuilder.ApplyConfiguration(new PlaceDbMap());
        modelBuilder.ApplyConfiguration(new ReviewDbMap());
    }
}