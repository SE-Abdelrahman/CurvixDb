using CurvixDb.Enums;
using CurvixDb.Models.Examinations;

namespace CurvixDb.Models.Patients
{
    internal class Patient
    {
        public int PatientId { get; set; }
        public string? MedicalNumber { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<PatientPhone> Phones { get; set; } = new HashSet<PatientPhone>();          // Navigation Properties
        public ICollection<Examination> Examinations { get; set; } = new HashSet<Examination>();     // Navigation Properties
    }
}
