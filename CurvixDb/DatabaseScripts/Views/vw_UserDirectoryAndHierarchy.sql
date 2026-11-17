CREATE OR ALTER VIEW dbo.vw_UserDirectoryAndHierarchy
AS
SELECT 
    u.UserId,
    u.Name AS UserName,
    u.Email,
    u.Role,
    u.CreatedAt,
    (
        SELECT STRING_AGG(up.PhoneNumber, ', ')
        FROM User_Phones up
        WHERE up.UserId = u.UserId
    ) AS PhoneNumbers,
    u.SupervisorId,
    sup.Name AS SupervisorName,
    (SELECT COUNT(*) FROM Examinations e WHERE e.CreatedByUserId = u.UserId) AS TotalCreatedExams,
    (SELECT COUNT(*) FROM Medical_Reports mr WHERE mr.UserId = u.UserId) AS TotalApprovedReports
FROM Users u
LEFT JOIN Users sup ON u.SupervisorId = sup.UserId;