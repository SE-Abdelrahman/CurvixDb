CREATE OR ALTER FUNCTION dbo.fn_CalculatePatientAge (@DateOfBirth DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DateOfBirth, GETUTCDATE()) -
           CASE 
               WHEN (MONTH(@DateOfBirth) > MONTH(GETUTCDATE())) OR 
                    (MONTH(@DateOfBirth) = MONTH(GETUTCDATE()) AND DAY(@DateOfBirth) > DAY(GETUTCDATE())) 
               THEN 1 ELSE 0 
           END;
END;