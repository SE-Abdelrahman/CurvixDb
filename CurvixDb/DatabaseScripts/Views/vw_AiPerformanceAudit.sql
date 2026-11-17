CREATE OR ALTER VIEW dbo.vw_AiPerformanceAudit
AS
SELECT 
    mr.ReportId,
    e.ExamId,
    mi.ViewType,
    ai.DetectedDeformity AS AiPrediction,
    mr.FinalDeformity AS DoctorDiagnosis,
    ai.MeasuredAngle AS AiAngle,
    mr.FinalAngle AS DoctorAngle,
    dbo.fn_GetAngleDifference(e.ExamId) AS AngleErrorMargin,
    ai.ConfidenceScore,
    mr.AIWasModified,
    u.Name AS ReviewingDoctor,
    mr.ApprovedAt
FROM Medical_Reports mr
INNER JOIN Examinations e ON mr.ExamId = e.ExamId
INNER JOIN AI_Predictions ai ON mr.PredictionId = ai.PredictionId
INNER JOIN Medical_Images mi ON ai.ImageId = mi.ImageId
INNER JOIN Users u ON mr.UserId = u.UserId;