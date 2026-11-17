IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Patients_Name' AND object_id = OBJECT_ID('dbo.Patients'))
CREATE NONCLUSTERED INDEX IX_Patients_Name 
ON dbo.Patients (Name);