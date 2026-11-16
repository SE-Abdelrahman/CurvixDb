CREATE OR ALTER FUNCTION dbo.fn_ClassifyDeformitySeverity (@DeformityType NVARCHAR(50), @Angle DECIMAL(10,2))
RETURNS NVARCHAR(30)
AS
BEGIN
    DECLARE @Severity NVARCHAR(50);

    IF @DeformityType = 'Normal' OR @Angle < 10
        SET @Severity = 'Normal';
    ELSE IF @DeformityType = 'Scoliosis'
    BEGIN
        IF @Angle < 25 SET @Severity = 'Mild';
        ELSE IF @Angle < 40 SET @Severity = 'Moderate';
        ELSE SET @Severity = 'Severe';
    END
    ELSE IF @DeformityType = 'Kyphosis'
    BEGIN
        IF @Angle <= 45 SET @Severity = 'Normal Range';
        ELSE IF @Angle <= 60 SET @Severity = 'Moderate Hyperkyphosis';
        ELSE SET @Severity = 'Severe Hyperkyphosis';
    END
    ELSE IF @DeformityType = 'Lordosis'
    BEGIN
        IF @Angle <= 60 SET @Severity = 'Normal Range';
        ELSE SET @Severity = 'Hyperlordosis';
    END
    ELSE
        SET @Severity = 'Unspecified';

    RETURN @Severity;
END;