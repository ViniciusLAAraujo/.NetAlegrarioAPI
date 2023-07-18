USE Alegrario
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spDayCell_Delete
/*EXEC AlegrarioAppSchema.spDayCell_Delete @UserId = , @CellDay = */
    @UserId INT,
    @CellDay DATE
AS
BEGIN
    DELETE FROM AlegrarioAppSchema.DayCells
        WHERE UserId = @UserId 
        AND CellDay = @CellDay
END