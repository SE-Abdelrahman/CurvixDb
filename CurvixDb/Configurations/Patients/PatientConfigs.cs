using CurvixDb.Models.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.Patients
{
    internal class PatientConfigs : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients", t =>
            {
                t.HasCheckConstraint("CK_Patients_Gender_Valid", "[Gender] IN ('Male', 'Female')");
                t.HasCheckConstraint("CK_Patients_DOB_Past", "[DateOfBirth] <= CAST(GETUTCDATE() AS date)");
            });

            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.MedicalNumber)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.HasIndex(p => p.MedicalNumber)
                   .IsUnique()
                   .HasDatabaseName("UQ_Patients_MedicalNumber");

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(500);

            builder.Property(p => p.Gender)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(p => p.DateOfBirth)
                   .HasColumnType("date");
        }
    }
}
