CREATE OR ALTER PROCEDURE dbo.sp_GetSystemDashboardStats
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Result Set 1: ملخص الأرقام العامة ودقة الـ AI
    SELECT 
        (SELECT COUNT(*) FROM Patients) AS TotalPatients,
        (SELECT COUNT(*) FROM Examinations WHERE (@StartDate IS NULL OR ExamDate >= @StartDate) AND (@EndDate IS NULL OR ExamDate <= @EndDate)) AS TotalExaminations,
        (SELECT COUNT(*) FROM Examinations WHERE Status = 'Pending') AS PendingExams,
        (SELECT COUNT(*) FROM Examinations WHERE Status = 'Analyzed') AS AwaitingDoctorApproval,
        (SELECT COUNT(*) FROM Examinations WHERE Status = 'Completed') AS CompletedExams,
        CAST(ISNULL(
            (SELECT COUNT(*) * 100.0 / NULLIF(COUNT(*), 0) FROM Medical_Reports WHERE AIWasModified = 0), 
        0) AS DECIMAL(5,2)) AS AiAgreementPercentage;

    -- Result Set 2: توزيع الحالات حسب نوع الانحراف المعتمد
    SELECT 
        FinalDeformity,
        COUNT(*) AS CaseCount,
        CAST(AVG(FinalAngle) AS DECIMAL(5,2)) AS AverageAngle
    FROM Medical_Reports
    WHERE (@StartDate IS NULL OR ApprovedAt >= @StartDate) AND (@EndDate IS NULL OR ApprovedAt <= @EndDate)
    GROUP BY FinalDeformity;
END;