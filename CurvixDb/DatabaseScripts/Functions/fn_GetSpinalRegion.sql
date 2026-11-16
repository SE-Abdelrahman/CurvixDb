CREATE OR ALTER FUNCTION dbo.fn_GetSpinalRegion (@UpperVertebra NVARCHAR(50), @LowerVertebra NVARCHAR(50))
RETURNS NVARCHAR(30)
AS
BEGIN
    IF @UpperVertebra IS NULL OR @LowerVertebra IS NULL
        RETURN 'N/A';

    DECLARE @U NVARCHAR(50) = UPPER(LEFT(LTRIM(@UpperVertebra), 1));
    DECLARE @L NVARCHAR(50) = UPPER(LEFT(LTRIM(@LowerVertebra), 1));

    IF @U = 'C' AND @L = 'C' RETURN 'Cervical';
    IF @U = 'T' AND @L = 'T' RETURN 'Thoracic';
    IF @U = 'L' AND @L = 'L' RETURN 'Lumbar';
    IF @U = 'T' AND @L = 'L' RETURN 'Thoracolumbar';
    IF @U = 'C' AND @L = 'T' RETURN 'Cervicothoracic';

    RETURN 'Mixed Region';
END;