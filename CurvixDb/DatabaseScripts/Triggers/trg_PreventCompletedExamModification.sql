CREATE OR ALTER TRIGGER dbo.trg_PreventCompletedExamModification
ON dbo.Medical_Reports
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- السماح بتحديث عمود AIWasModified فقط لو جاي من الـ Insert Trigger الداخلي
    IF UPDATE(AIWasModified) AND NOT EXISTS (SELECT 1 FROM deleted)
        RETURN;

    THROW 51002, 'Approved medical reports are immutable and cannot be modified or deleted.', 1;
END;