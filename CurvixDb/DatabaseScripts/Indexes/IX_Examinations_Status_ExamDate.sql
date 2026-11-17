IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Examinations_Status_ExamDate' AND object_id = OBJECT_ID('dbo.Examinations'))
CREATE NONCLUSTERED INDEX IX_Examinations_Status_ExamDate 
ON dbo.Examinations (Status, ExamDate)
INCLUDE (PatientId, CreatedByUserId);