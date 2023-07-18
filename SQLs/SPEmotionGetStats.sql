USE Alegrario
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spDayCell_GetEmotionStats
/*EXEC AlegrarioAppSchema.spDayCell_GetEmotionStats @UserId = 3 , @CellDay = '2023-07-22', @EmotionValue = 3 ,@IncludeAllEmotions = 1  */
    @UserId INT,
    @CellDay DATE, 
    @EmotionValue INT = NULL,
    @IncludeAllEmotions BIT = 0
AS
BEGIN
    IF @IncludeAllEmotions = 1
        BEGIN
            SELECT EmotionValue, COUNT(*) AS Amount, AVG(CAST(Score AS DECIMAL(10, 2))) AS Average
            FROM AlegrarioAppSchema.Emotions
                WHERE CellDay = @CellDay AND UserId = @UserId
                    GROUP BY EmotionValue
                    ORDER BY EmotionValue;
        END
    ELSE
        BEGIN

            DROP TABLE IF EXISTS  #FilteredEmotions;

            CREATE TABLE #FilteredEmotions (EmotionValue INT, Score DECIMAL(18, 4))

            IF @EmotionValue IS NULL
                BEGIN
                    INSERT INTO #FilteredEmotions (EmotionValue, Score)
                        SELECT EmotionValue, Score
                        FROM AlegrarioAppSchema.Emotions
                            WHERE CellDay = @CellDay AND UserId = @UserId;
                END
            ELSE
                BEGIN
                    INSERT INTO #FilteredEmotions (EmotionValue, Score)
                    SELECT EmotionValue, Score
                    FROM AlegrarioAppSchema.Emotions
                    WHERE CellDay = @CellDay AND UserId = @UserId AND EmotionValue = @EmotionValue;
                END

            SELECT CASE WHEN EmotionValue IS NULL THEN 0 ELSE @EmotionValue END AS EmotionValue,
                COUNT(*) AS Amount,
                AVG(Score) AS Average
            FROM #FilteredEmotions
            GROUP BY CASE WHEN EmotionValue IS NULL THEN 0 ELSE @EmotionValue END;
        END
END;
GO
