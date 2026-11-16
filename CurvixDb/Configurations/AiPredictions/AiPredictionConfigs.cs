using CurvixDb.Models.AiPredictions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.AiPredictions
{
    internal class AiPredictionConfigs : IEntityTypeConfiguration<AiPrediction>
    {
        public void Configure(EntityTypeBuilder<AiPrediction> builder)
        {
            builder.ToTable("AI_Predictions", t =>
            {
                t.HasCheckConstraint("CK_AiPredictions_Deformity_Valid", "[DetectedDeformity] IN ('Normal', 'Scoliosis', 'Kyphosis', 'Lordosis')");
                t.HasCheckConstraint("CK_AiPredictions_Confidence_Range", "[ConfidenceScore] >= 0 AND [ConfidenceScore] <= 1");
                t.HasCheckConstraint("CK_AiPredictions_Angle_Range", "[MeasuredAngle] >= 0 AND [MeasuredAngle] <= 180");
            });

            builder.HasKey(ai => ai.PredictionId);

            builder.Property(ai => ai.DetectedDeformity)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(ai => ai.ConfidenceScore)
                   .HasColumnType("decimal")
                   .HasPrecision(10,2);

            builder.Property(ai => ai.VisualOverlayPath)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(ai => ai.LowerVertebra)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(ai => ai.ApexVertebra)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(ai => ai.MeasuredAngle)
                   .HasColumnType("decimal")
                   .HasPrecision(10, 2);

            builder.Property(ai => ai.UpperVertebra)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(ai => ai.ImageId);

            builder.HasOne(ai => ai.MedicalImage)
                   .WithOne(mi => mi.AiPrediction)
                   .HasForeignKey<AiPrediction>(ai => ai.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
