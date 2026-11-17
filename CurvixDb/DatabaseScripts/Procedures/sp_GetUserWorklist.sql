CREATE OR ALTER PROCEDURE dbo.sp_GetUserWorklist
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Role NVARCHAR(50);
    SELECT @Role = Role FROM Users WHERE UserId = @UserId;

    SELECT 
        e.ExamId,
        e.ExamDate,
        e.Status,
        p.PatientId,
        p.MedicalNumber,
        p.Name AS PatientName,
        dbo.fn_CalculatePatientAge(p.DateOfBirth) AS PatientAge,
        ai.DetectedDeformity AS AiSuggestedDeformity,
        ai.MeasuredAngle AS AiMeasuredAngle
    FROM Examinations e
    INNER JOIN Patients p ON e.PatientId = p.PatientId
    LEFT JOIN Medical_Images mi ON e.ExamId = mi.ExamId AND mi.IsValid = 1
    LEFT JOIN AI_Predictions ai ON mi.ImageId = ai.ImageId
    WHERE 
        (@Role = 'Radiographer' AND e.Status = 'Pending')
        OR (@Role = 'Radiologist' AND e.Status = 'Analyzed')
        OR (@Role = 'Admin' AND e.Status IN ('Pending', 'Analyzed'))
    ORDER BY e.ExamDate ASC;
END;