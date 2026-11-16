using CurvixDb.Enums;
using CurvixDb.Models.MedicalImages;
using CurvixDb.Models.MedicalReports;
using CurvixDb.Models.Patients;
using CurvixDb.Models.Users;

namespace CurvixDb.Models.Examinations
{
    internal class Examination
    {
        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; } 
        public ExamStatus Status { get; set; } 

        public int PatientId { get; set; }        //FK
        public Patient Patient { get; set; }     // Navigation Properties

        public int CreatedByUserId { get; set; }        //FK
        public User CreatedByUser { get; set; }        // Navigation Properties

        public ICollection<MedicalImage> Images { get; set; } = new HashSet<MedicalImage>();         // Navigation Properties
        public MedicalReport? MedicalReport { get; set; }          // Navigation Properties
    }
}
