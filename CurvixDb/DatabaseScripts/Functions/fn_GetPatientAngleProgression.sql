CREATE OR ALTER FUNCTION dbo.fn_GetPatientAngleProgression (@PatientId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        e.ExamId,
        e.ExamDate,
        mr.FinalDeformity,
        mr.FinalAngle,
        mr.FinalAngle - LAG(mr.FinalAngle) OVER (ORDER BY e.ExamDate) AS AngleDelta
    FROM Examinations e
    INNER JOIN Medical_Reports mr ON e.ExamId = mr.ExamId
    WHERE e.PatientId = @PatientId
);