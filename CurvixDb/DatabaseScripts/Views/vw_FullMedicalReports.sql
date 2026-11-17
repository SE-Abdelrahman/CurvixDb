CREATE OR ALTER VIEW dbo.vw_FullMedicalReports
AS
SELECT 
    mr.ReportId,
    mr.ApprovedAt,
    e.ExamId,
    e.ExamDate,
    p.PatientId,
    p.MedicalNumber,
    p.Name AS PatientName,
    p.Gender AS PatientGender,
    dbo.fn_CalculatePatientAge(p.DateOfBirth) AS PatientAge,
    mi.ImagePath AS OriginalImagePath,
    mi.ViewType,
    ai.VisualOverlayPath,
    ai.DetectedDeformity AS AiDeformity,
    ai.MeasuredAngle AS AiAngle,
    ai.ConfidenceScore AS AiConfidence,
    ai.UpperVertebra,
    ai.ApexVertebra,
    ai.LowerVertebra,
    dbo.fn_GetSpinalRegion(ai.UpperVertebra, ai.LowerVertebra) AS SpinalRegion,
    mr.FinalDeformity,
    mr.FinalAngle,
    dbo.fn_ClassifyDeformitySeverity(mr.FinalDeformity, mr.FinalAngle) AS SeverityClassification,
    dbo.fn_GetAngleDifference(e.ExamId) AS AngleDifferenceFromAi,
    mr.AIWasModified,
    mr.ReportText,
    u.Name AS ApprovedByDoctor
FROM Medical_Reports mr
INNER JOIN Examinations e ON mr.ExamId = e.ExamId
INNER JOIN Patients p ON e.PatientId = p.PatientId
INNER JOIN AI_Predictions ai ON mr.PredictionId = ai.PredictionId
INNER JOIN Medical_Images mi ON ai.ImageId = mi.ImageId
INNER JOIN Users u ON mr.UserId = u.UserId;