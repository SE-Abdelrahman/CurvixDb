using CurvixDb.Enums;
using CurvixDb.Models.Examinations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.Examinations
{
    internal class ExaminationConfigs : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            builder.ToTable("Examinations", t =>
            {
                t.HasCheckConstraint("CK_Examinations_Status_Valid", "[Status] IN ('Pending', 'Analyzed', 'Completed', 'Cancelled')");
            });

            builder.HasKey(e => e.ExamId);

            builder.Property(e => e.ExamDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.Status)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50)
                   .HasDefaultValue(ExamStatus.Pending);

            builder.HasOne(e => e.Patient)
                   .WithMany(p => p.Examinations)
                   .HasForeignKey(e => e.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CreatedByUser)
                   .WithMany(u => u.CreatedExaminations)
                   .HasForeignKey(e => e.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
