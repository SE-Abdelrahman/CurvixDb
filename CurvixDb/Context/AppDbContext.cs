using CurvixDb.Models.AiPredictions;
using CurvixDb.Models.Examinations;
using CurvixDb.Models.MedicalImages;
using CurvixDb.Models.MedicalReports;
using CurvixDb.Models.Patients;
using CurvixDb.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CurvixDb.Context
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientPhone> PatientPhones { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<MedicalImage> MedicalImages { get; set; }
        public DbSet<AiPrediction> AiPredictions { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CurvixDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //OR
            /*
            modelBuilder.ApplyConfiguration(new UserConfigs());
            modelBuilder.ApplyConfiguration(new UserPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new PatientPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalImageConfiguration());
            modelBuilder.ApplyConfiguration(new AiPredictionConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalReportConfiguration());
            */
        }
    }
}