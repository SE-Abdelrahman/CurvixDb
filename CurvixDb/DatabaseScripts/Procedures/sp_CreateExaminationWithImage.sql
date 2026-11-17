CREATE OR ALTER PROCEDURE dbo.sp_CreateExaminationWithImage
    @PatientId INT,
    @CreatedByUserId INT,
    @ImagePath NVARCHAR(500),
    @ViewType NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Examinations (PatientId, CreatedByUserId, ExamDate, Status)
        VALUES (@PatientId, @CreatedByUserId, GETUTCDATE(), 'Pending');

        DECLARE @NewExamId INT = SCOPE_IDENTITY();

        INSERT INTO Medical_Images (ExamId, ImagePath, ViewType, IsValid)
        VALUES (@NewExamId, @ImagePath, @ViewType, 1);

        DECLARE @NewImageId INT = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
        SELECT @NewExamId AS ExamId, @NewImageId AS ImageId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;