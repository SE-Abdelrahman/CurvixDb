CREATE OR ALTER PROCEDURE dbo.sp_RegisterPatientWithPhones
    @MedicalNumber NVARCHAR(50),
    @Name NVARCHAR(250),
    @Gender NVARCHAR(20),
    @DateOfBirth DATE,
    @PrimaryPhone NVARCHAR(20),
    @SecondaryPhone NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Patients WHERE MedicalNumber = @MedicalNumber)
            THROW 50002, 'Patient with this Medical Number already exists.', 1;

        INSERT INTO Patients (MedicalNumber, Name, Gender, DateOfBirth)
        VALUES (@MedicalNumber, @Name, @Gender, @DateOfBirth);

        DECLARE @NewPatientId INT = SCOPE_IDENTITY();

        INSERT INTO Patient_Phones (PatientId, PhoneNumber)
        VALUES (@NewPatientId, @PrimaryPhone);

        IF @SecondaryPhone IS NOT NULL AND LTRIM(RTRIM(@SecondaryPhone)) <> ''
            INSERT INTO Patient_Phones (PatientId, PhoneNumber)
            VALUES (@NewPatientId, @SecondaryPhone);

        COMMIT TRANSACTION;
        SELECT @NewPatientId AS PatientId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;