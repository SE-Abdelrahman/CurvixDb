using CurvixDb.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurvixDb.Configurations.Users
{
    internal class UserPhoneConfigs : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("User_Phones", t =>
            {
                t.HasCheckConstraint("CK_UserPhones_Format", "LEN([PhoneNumber]) >= 7 AND [PhoneNumber] NOT LIKE '%[^0-9+]%'");
            });

            builder.HasKey(up => new { up.UserId, up.PhoneNumber });

            builder.Property(up => up.PhoneNumber)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(20);

            builder.HasOne(up => up.User)
                   .WithMany(u => u.Phones)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
