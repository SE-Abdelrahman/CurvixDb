using CurvixDb.Models.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.Patients
{
    internal class PatientPhoneConfigs : IEntityTypeConfiguration<PatientPhone>
    {
        public void Configure(EntityTypeBuilder<PatientPhone> builder)
        {
            builder.ToTable("Patient_Phones", t =>
            {
                t.HasCheckConstraint("CK_PatientPhones_Format", "LEN([PhoneNumber]) >= 7 AND [PhoneNumber] NOT LIKE '%[^0-9+]%'");
            });

            builder.HasKey(pp => new { pp.PatientId, pp.PhoneNumber });

            builder.Property(pp => pp.PhoneNumber)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(20);

            builder.HasOne(pp => pp.Patient)
                   .WithMany(p => p.Phones)
                   .HasForeignKey(pp => pp.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
