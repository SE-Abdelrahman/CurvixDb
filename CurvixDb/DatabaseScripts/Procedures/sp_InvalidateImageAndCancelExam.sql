CREATE OR ALTER PROCEDURE dbo.sp_InvalidateImageAndCancelExam
    @ImageId INT,
    @CancelExam BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @ExamId INT;
        SELECT @ExamId = ExamId FROM Medical_Images WHERE ImageId = @ImageId;

        IF @ExamId IS NULL
            THROW 50003, 'Medical image not found.', 1;

        UPDATE Medical_Images
        SET IsValid = 0
        WHERE ImageId = @ImageId;

        IF @CancelExam = 1 AND NOT EXISTS (SELECT 1 FROM Medical_Images WHERE ExamId = @ExamId AND IsValid = 1)
        BEGIN
            UPDATE Examinations
            SET Status = 'Cancelled'
            WHERE ExamId = @ExamId;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;