using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Models.DbMaps;

public class ReviewDbMap : IEntityTypeConfiguration<ReviewDbModel>
{
    public void Configure(EntityTypeBuilder<ReviewDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.ReviewText).HasColumnType("VARCHAR(300)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.ReviewDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.Rate).HasColumnType("NUMERIC(5,2)").HasDefaultValue(0.0f).IsRequired();
        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.PlaceId);
        builder.HasOne(u => u.User)
               .WithMany(u => u.Reviews)
               .HasForeignKey(k => k.UserId);
        builder.HasOne(u => u.Place)
               .WithMany(u => u.Reviews)
               .HasForeignKey(k => k.PlaceId);
    }
}
