CREATE OR ALTER FUNCTION dbo.fn_GetAngleDifference (@ExamId INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Diff DECIMAL(10,2);

    SELECT @Diff = ABS(mr.FinalAngle - ai.MeasuredAngle)
    FROM Medical_Reports mr
    INNER JOIN AI_Predictions ai ON mr.PredictionId = ai.PredictionId
    WHERE mr.ExamId = @ExamId;

    RETURN ISNULL(@Diff, 0);
END;