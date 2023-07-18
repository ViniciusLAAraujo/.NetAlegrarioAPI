USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spDayCell_Insert
/*EXEC AlegrarioAppSchema.spDayCell_Insert @UserId = 3, @CellDay = '2023-07-24' */
    @UserId INT
    , @CellDay DATE
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM AlegrarioAppSchema.DayCells WHERE UserId = @UserId AND CellDay = @CellDay )
        BEGIN
            INSERT INTO AlegrarioAppSchema.DayCells (
                [UserId],
                [CellDay]
            ) VALUES (
                @UserId,
                @CellDay
            )
        END
END