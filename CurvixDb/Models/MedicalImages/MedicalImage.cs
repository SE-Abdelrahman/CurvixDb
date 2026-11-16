using CurvixDb.Enums;
using CurvixDb.Models.AiPredictions;
using CurvixDb.Models.Examinations;

namespace CurvixDb.Models.MedicalImages
{
    internal class MedicalImage
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public ViewType ViewType { get; set; }
        public bool IsValid { get; set; } 

        public int ExamId { get; set; }        //FK
        public Examination Examination { get; set; }          // Navigation Property

        public AiPrediction? AiPrediction { get; set; }          // Navigation Property
    }
}
