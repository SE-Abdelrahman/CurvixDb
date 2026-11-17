CREATE OR ALTER PROCEDURE dbo.sp_SaveAiPrediction
    @ImageId INT,
    @DetectedDeformity NVARCHAR(50),
    @ConfidenceScore DECIMAL(10,2),
    @VisualOverlayPath NVARCHAR(500),
    @UpperVertebra NVARCHAR(10) = NULL,
    @ApexVertebra NVARCHAR(10) = NULL,
    @LowerVertebra NVARCHAR(10) = NULL,
    @MeasuredAngle DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ExamId INT, @IsValid BIT;
        SELECT @ExamId = ExamId, @IsValid = IsValid
        FROM Medical_Images
        WHERE ImageId = @ImageId;

        IF @ExamId IS NULL OR @IsValid = 0
            THROW 50004, 'Image does not exist or is marked as invalid.', 1;

        INSERT INTO AI_Predictions (
            ImageId, DetectedDeformity, ConfidenceScore, VisualOverlayPath,
            UpperVertebra, ApexVertebra, LowerVertebra, MeasuredAngle
        )
        VALUES (
            @ImageId, @DetectedDeformity, @ConfidenceScore, @VisualOverlayPath,
            @UpperVertebra, @ApexVertebra, @LowerVertebra, @MeasuredAngle
        );

        DECLARE @NewPredictionId INT = SCOPE_IDENTITY();

        UPDATE Examinations
        SET Status = 'Analyzed'
        WHERE ExamId = @ExamId AND Status = 'Pending';

        COMMIT TRANSACTION;
        SELECT @NewPredictionId AS PredictionId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;