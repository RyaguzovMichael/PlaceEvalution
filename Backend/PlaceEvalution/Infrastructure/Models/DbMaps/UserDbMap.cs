using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceEvalution.API.Infrastructure.Models.DbModels;

namespace PlaceEvalution.API.Infrastructure.Models.DbMaps;

public class UserDbMap : IEntityTypeConfiguration<UserDbModel>
{
    public void Configure(EntityTypeBuilder<UserDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.Login).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.Password).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").HasDefaultValue("Default").IsRequired();
        builder.Property(p => p.Surname).HasColumnType("VARCHAR(100)").HasDefaultValue("User").IsRequired();
        builder.HasMany(u => u.Reviews)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId);
    }
}