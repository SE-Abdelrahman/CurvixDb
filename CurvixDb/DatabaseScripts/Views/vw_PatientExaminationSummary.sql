CREATE OR ALTER VIEW dbo.vw_PatientExaminationSummary
AS
SELECT 
    p.PatientId,
    p.MedicalNumber,
    p.Name AS PatientName,
    p.Gender,
    p.DateOfBirth,
    dbo.fn_CalculatePatientAge(p.DateOfBirth) AS Age,
    (
        SELECT STRING_AGG(pp.PhoneNumber, ', ')
        FROM Patient_Phones pp
        WHERE pp.PatientId = p.PatientId
    ) AS PhoneNumbers,
    COUNT(e.ExamId) AS TotalExaminations,
    MAX(e.ExamDate) AS LastExamDate,
    (
        SELECT TOP 1 mr.FinalDeformity
        FROM Examinations e2
        INNER JOIN Medical_Reports mr ON e2.ExamId = mr.ExamId
        WHERE e2.PatientId = p.PatientId
        ORDER BY e2.ExamDate DESC
    ) AS LatestDiagnosis,
    (
        SELECT TOP 1 mr.FinalAngle
        FROM Examinations e2
        INNER JOIN Medical_Reports mr ON e2.ExamId = mr.ExamId
        WHERE e2.PatientId = p.PatientId
        ORDER BY e2.ExamDate DESC
    ) AS LatestApprovedAngle
FROM Patients p
LEFT JOIN Examinations e ON p.PatientId = e.PatientId
GROUP BY p.PatientId, p.MedicalNumber, p.Name, p.Gender, p.DateOfBirth;