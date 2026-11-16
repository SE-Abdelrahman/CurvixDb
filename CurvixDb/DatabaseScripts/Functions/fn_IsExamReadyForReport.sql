CREATE OR ALTER FUNCTION dbo.fn_IsExamReadyForReport (@ExamId INT)
RETURNS BIT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Medical_Images mi
        INNER JOIN AI_Predictions ai ON mi.ImageId = ai.ImageId
        WHERE mi.ExamId = @ExamId AND mi.IsValid = 1
    )
        RETURN 1;

    RETURN 0;
END;