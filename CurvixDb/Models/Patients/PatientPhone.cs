namespace CurvixDb.Models.Patients
{
    internal class PatientPhone
    {
        public int PatientId { get; set; }
        public string PhoneNumber { get; set; }

        public Patient Patient { get; set; }     // Navigation Property
    }
}
