USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spEmotion_Get
/*EXEC AlegrarioAppSchema.spEmotion_Get  @UserId=3 */
/*EXEC AlegrarioAppSchema.spEmotion_Get  @UserId=3 , @MonthCells=7 */
/*EXEC AlegrarioAppSchema.spEmotion_Get  @UserId=3 , @HourId =4 */
/*EXEC AlegrarioAppSchema.spEmotion_Get  @UserId=3 , @CellDay = '2023-07-22' */
/*EXEC AlegrarioAppSchema.spEmotion_Get  @UserId=3 , @CellDay = '2023-07-22' , @EmotionValue = 3 */
    @UserId INT
    , @HourId INT = NULL
    , @MonthCells INT = NULL
    , @CellDay DATE =NULL
    , @EmotionValue INT = NULL
    
AS
BEGIN

    SELECT [Emotions].[HourId],
    [Emotions].[CellDay],
    [Emotions].[UserId],
    [Emotions].[EmotionValue],
    [Emotions].[Comment],
    [Emotions].[Score] FROM AlegrarioAppSchema.Emotions AS Emotions
    WHERE [Emotions].[UserId] = @UserId
        AND (@MonthCells IS NULL OR MONTH([Emotions].[CellDay]) = @MonthCells)
        AND (@CellDay IS NULL OR [Emotions].[CellDay] = @CellDay)
        AND [Emotions].[HourId] = ISNULL(@HourId, [Emotions].[HourId])
        AND [Emotions].[EmotionValue] = ISNULL(@EmotionValue, [Emotions].[EmotionValue])

END