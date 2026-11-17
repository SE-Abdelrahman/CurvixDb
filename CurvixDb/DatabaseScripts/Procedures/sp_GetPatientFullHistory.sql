CREATE OR ALTER PROCEDURE dbo.sp_GetPatientFullHistory
    @PatientId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Result Set 1: البيانات الأساسية للمريض
    SELECT 
        PatientId,
        MedicalNumber,
        Name,
        Gender,
        DateOfBirth,
        dbo.fn_CalculatePatientAge(DateOfBirth) AS Age
    FROM Patients
    WHERE PatientId = @PatientId;

    -- Result Set 2: أرقام تليفونات المريض
    SELECT PhoneNumber
    FROM Patient_Phones
    WHERE PatientId = @PatientId;

    -- Result Set 3: سجل الفحوصات والتقارير الطبية المعتمدة
    SELECT 
        e.ExamId,
        e.ExamDate,
        e.Status,
        mr.ReportId,
        mr.FinalDeformity,
        mr.FinalAngle,
        dbo.fn_ClassifyDeformitySeverity(mr.FinalDeformity, mr.FinalAngle) AS Severity,
        dbo.fn_GetSpinalRegion(ai.UpperVertebra, ai.LowerVertebra) AS SpinalRegion,
        mr.ReportText,
        mr.ApprovedAt,
        u.Name AS ApprovedByDoctor
    FROM Examinations e
    LEFT JOIN Medical_Reports mr ON e.ExamId = mr.ExamId
    LEFT JOIN AI_Predictions ai ON mr.PredictionId = ai.PredictionId
    LEFT JOIN Users u ON mr.UserId = u.UserId
    WHERE e.PatientId = @PatientId
    ORDER BY e.ExamDate DESC;
END;