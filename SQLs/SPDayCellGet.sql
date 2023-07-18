USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spDayCell_Get
/*EXEC AlegrarioAppSchema.spDayCell_Get  @UserId=2 */
/*EXEC AlegrarioAppSchema.spDayCell_Get  @UserId=2 , @CellDay = '2023-07-17' */
/*EXEC AlegrarioAppSchema.spDayCell_Get  @UserId=2, @MonthCells=8  */
    @UserId INT 
    , @CellDay DATE  = NULL
    , @MonthCells INT = NULL
AS
BEGIN
    IF @MonthCells IS NULL AND @CellDay IS NULL
            SET @MonthCells = MONTH(GETDATE());

    SELECT [DayCells].[CellDay],
    [DayCells].[UserId] 
    FROM AlegrarioAppSchema.DayCells AS DayCells
    WHERE DayCells.UserId = @UserId
    AND ( @CellDay IS NULL OR DayCells.CellDay = @CellDay)
    AND (@MonthCells IS NULL OR MONTH(DayCells.CellDay) = @MonthCells)

END