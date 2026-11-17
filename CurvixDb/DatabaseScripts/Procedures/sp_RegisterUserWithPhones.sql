CREATE OR ALTER PROCEDURE dbo.sp_RegisterUserWithPhones
    @Name NVARCHAR(250),
    @Email NVARCHAR(250),
    @PasswordHash NVARCHAR(250),
    @Role NVARCHAR(50),
    @SupervisorId INT = NULL,
    @PrimaryPhone NVARCHAR(20),
    @SecondaryPhone NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
            THROW 50001, 'Email is already registered.', 1;

        INSERT INTO Users (Name, Email, PasswordHash, Role, SupervisorId, CreatedAt)
        VALUES (@Name, @Email, @PasswordHash, @Role, @SupervisorId, GETUTCDATE());

        DECLARE @NewUserId INT = SCOPE_IDENTITY();

        INSERT INTO User_Phones (UserId, PhoneNumber)
        VALUES (@NewUserId, @PrimaryPhone);

        IF @SecondaryPhone IS NOT NULL AND LTRIM(RTRIM(@SecondaryPhone)) <> ''
            INSERT INTO User_Phones (UserId, PhoneNumber)
            VALUES (@NewUserId, @SecondaryPhone);

        COMMIT TRANSACTION;
        SELECT @NewUserId AS UserId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;