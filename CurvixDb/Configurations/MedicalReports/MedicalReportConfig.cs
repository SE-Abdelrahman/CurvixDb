using CurvixDb.Models.MedicalReports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.MedicalReports
{
    internal class MedicalReportConfig : IEntityTypeConfiguration<MedicalReport>
    {
        public void Configure(EntityTypeBuilder<MedicalReport> builder)
        {
            builder.ToTable("Medical_Reports", t =>
            {
                t.HasCheckConstraint("CK_MedicalReports_Deformity_Valid", "[FinalDeformity] IN ('Normal', 'Scoliosis', 'Kyphosis', 'Lordosis')");
                t.HasCheckConstraint("CK_MedicalReports_Angle_Range", "[FinalAngle] >= 0 AND [FinalAngle] <= 180");
            });

            builder.HasKey(mr => mr.ReportId);

            builder.Property(mr => mr.FinalDeformity)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(mr => mr.FinalAngle)
                   .HasColumnType("decimal")
                   .HasPrecision(10,2);

            builder.Property(mr => mr.ApprovedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(mr => mr.ReportText)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(mr => mr.AiWasModified)
                   .HasDefaultValue(false);

            builder.Property(mr => mr.ExamId);

            builder.Property(mr => mr.PredictionId);

            builder.Property(mr => mr.UserId);

            builder.HasOne(mr => mr.Examination)
                   .WithOne(e => e.MedicalReport)
                   .HasForeignKey<MedicalReport>(mr => mr.ExamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mr => mr.AiPrediction)
                   .WithOne(ai => ai.MedicalReport)
                   .HasForeignKey<MedicalReport>(mr => mr.PredictionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mr => mr.ApprovedByUser)
                   .WithMany(u => u.ApprovedReports)
                   .HasForeignKey(mr => mr.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
