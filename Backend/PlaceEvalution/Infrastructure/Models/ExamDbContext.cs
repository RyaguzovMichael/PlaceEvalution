using Microsoft.EntityFrameworkCore;
using PlaceEvolution.API.Common.Extensions;
using PlaceEvolution.API.Infrastructure.Models.DbMaps;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Models;

public class DataBaseContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<PlaceDbModel> Places { get; set; }
    public DbSet<ReviewDbModel> Reviews { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserDbMap());
        modelBuilder.ApplyConfiguration(new PlaceDbMap());
        modelBuilder.ApplyConfiguration(new ReviewDbMap());

        modelBuilder.Entity<UserDbModel>().HasData(
            new UserDbModel[]
            {
                new UserDbModel()
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Login = "admin",
                    Name = "Admin",
                    Password = "admin".Hash(),
                    Role = Domain.UserRoles.Admin,
                    Surname = ""
                }
            }
            );
    }
}