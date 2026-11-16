using CurvixDb.Enums;
using CurvixDb.Models.AiPredictions;
using CurvixDb.Models.Examinations;
using CurvixDb.Models.Users;

namespace CurvixDb.Models.MedicalReports
{
    internal class MedicalReport
    {
        public int ReportId { get; set; }
        public DeformityType FinalDeformity { get; set; }
        public decimal FinalAngle { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string ReportText { get; set; }
        public bool AiWasModified { get; set; }

        public int ExamId { get; set; }         //FK
        public Examination Examination { get; set; }          // Navigation Property

        public int PredictionId { get; set; }        //FK
        public AiPrediction AiPrediction { get; set; }         // Navigation Property

        public int UserId { get; set; }          //FK
        public User ApprovedByUser { get; set; }    // Navigation Property
    }
}
