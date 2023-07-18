USE Alegrario
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spEmotion_Delete
/*EXEC AlegrarioAppSchema.spEmotion_Delete @UserId = , @CellDay = , @HourId=*/
    @UserId INT,
    @CellDay DATE,
    @HourId INT
AS
BEGIN
    DELETE FROM AlegrarioAppSchema.Emotions
    WHERE UserId = @UserId 
        AND CellDay = @CellDay
        AND HourId = @HourId
END