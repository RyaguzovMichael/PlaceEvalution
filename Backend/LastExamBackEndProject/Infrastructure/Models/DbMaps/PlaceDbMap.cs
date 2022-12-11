using LastExamBackEndProject.Infrastructure.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LastExamBackEndProject.Infrastructure.Models.DbMaps;

public class PlaceDbMap : IEntityTypeConfiguration<PlaceDbModel>
{
    public void Configure(EntityTypeBuilder<PlaceDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.Description).HasColumnType("VARCHAR(300)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.Title).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.TitlePhotoLink).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.Rate).HasColumnType("NUMERIC(5,2)").HasDefaultValue(0.0f).IsRequired();
        builder.HasMany<ReviewDbModel>(u => u.Reviews)
               .WithOne(r => r.Place)
               .HasForeignKey(r => r.PlaceId);
    }
}
