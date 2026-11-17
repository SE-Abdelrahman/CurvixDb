CREATE OR ALTER PROCEDURE dbo.sp_ApproveMedicalReport
    @ExamId INT,
    @PredictionId INT,
    @DoctorUserId INT,
    @FinalDeformity NVARCHAR(50),
    @FinalAngle DECIMAL(10,2),
    @ReportText NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF dbo.fn_IsExamReadyForReport(@ExamId) = 0
            THROW 50005, 'Examination is not ready for report approval (missing valid image or AI prediction).', 1;

        DECLARE @AiDeformity NVARCHAR(20), @AiAngle DECIMAL(5,2);
        SELECT @AiDeformity = DetectedDeformity, @AiAngle = MeasuredAngle
        FROM AI_Predictions
        WHERE PredictionId = @PredictionId;

        DECLARE @WasModified BIT = CASE 
            WHEN @FinalDeformity <> @AiDeformity OR ABS(@FinalAngle - @AiAngle) > 0.01 THEN 1 
            ELSE 0 
        END;

        INSERT INTO Medical_Reports (
            ExamId, PredictionId, UserId, FinalDeformity, FinalAngle,
            ReportText, AIWasModified, ApprovedAt
        )
        VALUES (
            @ExamId, @PredictionId, @DoctorUserId, @FinalDeformity, @FinalAngle,
            @ReportText, @WasModified, GETUTCDATE()
        );

        DECLARE @NewReportId INT = SCOPE_IDENTITY();

        UPDATE Examinations
        SET Status = 'Completed'
        WHERE ExamId = @ExamId;

        COMMIT TRANSACTION;
        SELECT @NewReportId AS ReportId, @WasModified AS AIWasModified;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;