USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spEmotion_Upsert
/*EXEC AlegrarioAppSchema.spEmotion_Upsert  @UserId =3   , @CellDay ='2023-09-01' ,  @HourId =9  , @EmotionValue =1   , @Comment ='Feeling happy'   , @Score = 4 */
    @UserId INT
    , @CellDay DATE
    , @HourId INT 
    , @EmotionValue INT
    , @Comment NVARCHAR(50)
    , @Score INT
AS
BEGIN

    IF NOT EXISTS (SELECT * FROM AlegrarioAppSchema.DayCells WHERE UserId = @UserId AND @CellDay = CellDay )
        BEGIN
            INSERT  INTO AlegrarioAppSchema.DayCells (
                [UserId],
                [CellDay]
            ) VALUES (
                @UserId,
                @CellDay
            )
            INSERT  INTO AlegrarioAppSchema.Emotions (
                [UserId],
                [CellDay],
                [HourId],
                [EmotionValue],
                [Comment],
                [Score]
            ) VALUES (
                @UserId,
                @CellDay,
                @HourId,
                @EmotionValue,
                @Comment,
                @Score
            )
        END
    ELSE
        BEGIN
            IF NOT EXISTS (SELECT * FROM AlegrarioAppSchema.Emotions WHERE UserId = @UserId AND @CellDay = CellDay AND HourId = @HourId)
                BEGIN
                    INSERT  INTO AlegrarioAppSchema.Emotions (
                    [UserId],
                    [CellDay],
                    [HourId],
                    [EmotionValue],
                    [Comment],
                    [Score]
                ) VALUES (
                    @UserId,
                    @CellDay,
                    @HourId,
                    @EmotionValue,
                    @Comment,
                    @Score
                )
                    
                END
            ELSE
                BEGIN
                    UPDATE AlegrarioAppSchema.Emotions
                            SET EmotionValue = @EmotionValue,
                                Comment = @Comment,
                                Score = @Score
                            WHERE UserId = @UserId 
                                AND CellDay =  @CellDay
                                AND  HourId = @HourId
                END
        END
END
GO
