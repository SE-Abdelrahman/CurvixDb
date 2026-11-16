using CurvixDb.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.Users
{
    internal class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", t =>
            {
                t.HasCheckConstraint("CK_Users_Email_Format", "[Email] LIKE '%_@_%._%' AND [Email] NOT LIKE '% %'");
                t.HasCheckConstraint("CK_Users_No_Self_Supervisor", "[SupervisorId] IS NULL OR [SupervisorId] <> [UserId]");
                t.HasCheckConstraint("CK_Users_Role_Valid", "[Role] IN ('Admin', 'Radiologist', 'Radiographer')");
            });

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("UQ_Users_Email");

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(u => u.Role)
                   .HasConversion<string>()
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(u => u.Supervisor)
                   .WithMany(u => u.Subordinates)
                   .HasForeignKey(u => u.SupervisorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}