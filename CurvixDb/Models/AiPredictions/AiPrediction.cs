using CurvixDb.Enums;
using CurvixDb.Models.MedicalImages;
using CurvixDb.Models.MedicalReports;

namespace CurvixDb.Models.AiPredictions
{
    internal class AiPrediction
    {
        public int PredictionId { get; set; }
        public DeformityType DetectedDeformity { get; set; }
        public decimal ConfidenceScore { get; set; }
        public string VisualOverlayPath { get; set; }
        public string? LowerVertebra { get; set; }
        public string? ApexVertebra { get; set; }
        public decimal MeasuredAngle { get; set; }
        public string? UpperVertebra { get; set; }

        public int ImageId { get; set; }      //FK
        public MedicalImage MedicalImage { get; set; }         // Navigation Property

        public MedicalReport? MedicalReport { get; set; }          // Navigation Property
    }
}
