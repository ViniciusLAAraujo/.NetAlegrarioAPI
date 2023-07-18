USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spDayCellWeek_Get
/*EXEC AlegrarioAppSchema.spDayCellWeek_Get  @UserId=2 */
    @UserId INT 

AS
BEGIN
    SELECT  [DayCells].[UserId],
            [DayCells].[CellDay] 
    FROM AlegrarioAppSchema.DayCells AS DayCells
        WHERE DATEPART(WEEK, [CellDay]) = DATEPART(WEEK, GETDATE())
            AND DayCells.UserId = @UserId
            ORDER BY 
                CASE WHEN DATEPART(WEEKDAY, [CellDay]) = 1 THEN 7 ELSE DATEPART(WEEKDAY, [CellDay]) - 1 END;
END