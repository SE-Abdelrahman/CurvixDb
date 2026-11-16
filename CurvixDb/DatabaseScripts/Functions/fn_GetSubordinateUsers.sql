CREATE OR ALTER FUNCTION dbo.fn_GetSubordinateUsers (@SupervisorId INT)
RETURNS TABLE
AS
RETURN
(
    WITH UserHierarchy AS (
        SELECT UserId, Name, Email, Role, SupervisorId, 1 AS HierarchyLevel
        FROM Users
        WHERE SupervisorId = @SupervisorId

        UNION ALL

        SELECT u.UserId, u.Name, u.Email, u.Role, u.SupervisorId, uh.HierarchyLevel + 1
        FROM Users u
        INNER JOIN UserHierarchy uh ON u.SupervisorId = uh.UserId
    )
    SELECT * FROM UserHierarchy
);