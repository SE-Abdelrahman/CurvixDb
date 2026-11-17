CREATE OR ALTER TRIGGER dbo.trg_AfterInsertMedicalReport
ON dbo.Medical_Reports
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. حساب AIWasModified أوتوماتيك بمقارنة قرار الدكتور بنتيجة الـ AI
    UPDATE mr
    SET mr.AIWasModified = CASE 
        WHEN i.FinalDeformity <> ai.DetectedDeformity 
             OR ABS(i.FinalAngle - ai.MeasuredAngle) > 0.01 
        THEN 1 
        ELSE 0 
    END
    FROM dbo.Medical_Reports mr
    INNER JOIN inserted i ON mr.ReportId = i.ReportId
    INNER JOIN dbo.AI_Predictions ai ON i.PredictionId = ai.PredictionId;

    -- 2. تقفيل الفحص أوتوماتيك وتحويل حالته إلى Completed
    UPDATE e
    SET e.Status = 'Completed'
    FROM dbo.Examinations e
    INNER JOIN inserted i ON e.ExamId = i.ExamId
    WHERE e.Status <> 'Completed';
END;