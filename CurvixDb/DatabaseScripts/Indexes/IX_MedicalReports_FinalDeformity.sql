IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_MedicalReports_FinalDeformity' AND object_id = OBJECT_ID('dbo.Medical_Reports'))
CREATE NONCLUSTERED INDEX IX_MedicalReports_FinalDeformity 
ON dbo.Medical_Reports (FinalDeformity)
INCLUDE (FinalAngle, AIWasModified);