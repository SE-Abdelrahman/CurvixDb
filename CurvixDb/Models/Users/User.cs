using CurvixDb.Enums;
using CurvixDb.Models.Examinations;
using CurvixDb.Models.MedicalReports;

namespace CurvixDb.Models.Users
{
    internal class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? SupervisorId { get; set; }     // FK
        public User? Supervisor { get; set; }     // Navigation Properties
        public ICollection<User> Subordinates { get; set; } = new HashSet<User>();

        public ICollection<UserPhone> Phones { get; set; } = new HashSet<UserPhone>();     // Navigation Properties
        public ICollection<Examination> CreatedExaminations { get; set; } = new HashSet<Examination>();      // Navigation Properties
        public ICollection<MedicalReport> ApprovedReports { get; set; } = new HashSet<MedicalReport>();     // Navigation Properties
    }
}
