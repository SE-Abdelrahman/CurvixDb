CREATE OR ALTER TRIGGER dbo.trg_AfterInsertAiPrediction
ON dbo.AI_Predictions
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- منع حفظ تنبؤ AI لصورة غير صالحة (IsValid = 0)
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN dbo.Medical_Images mi ON i.ImageId = mi.ImageId
        WHERE mi.IsValid = 0
    )
    BEGIN
        THROW 51001, 'Cannot insert AI prediction for an invalid medical image.', 1;
    END

    -- تحديث حالة الفحص أوتوماتيك إلى Analyzed لو كان لسه Pending
    UPDATE e
    SET e.Status = 'Analyzed'
    FROM dbo.Examinations e
    INNER JOIN dbo.Medical_Images mi ON e.ExamId = mi.ExamId
    INNER JOIN inserted i ON mi.ImageId = i.ImageId
    WHERE e.Status = 'Pending';
END;