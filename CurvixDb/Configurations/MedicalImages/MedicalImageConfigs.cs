using CurvixDb.Models.MedicalImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.MedicalImages
{
    internal class MedicalImageConfigs : IEntityTypeConfiguration<MedicalImage>
    {
        public void Configure(EntityTypeBuilder<MedicalImage> builder)
        {
            builder.ToTable("Medical_Images", t =>
            {
                t.HasCheckConstraint("CK_MedicalImages_ViewType_Valid", "[ViewType] IN ('AP', 'PA', 'Lateral')");
            });

            builder.HasKey(mi => mi.ImageId);

            builder.Property(mi => mi.ImagePath)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(mi => mi.ViewType)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(mi => mi.IsValid)
                   .HasDefaultValue(true);

            builder.Property(mi => mi.ExamId);

            builder.HasOne(mi => mi.Examination)
                   .WithMany(e => e.Images)
                   .HasForeignKey(mi => mi.ExamId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
