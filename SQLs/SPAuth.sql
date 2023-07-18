USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spRegistration_Upsert
/*EXEC AlegrarioAppSchema.spRegistration_Upsert */
    @Email NVARCHAR(50),
	@PasswordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX)
AS
BEGIN

    IF NOT EXISTS (SELECT * FROM AlegrarioAppSchema.Auth WHERE Email = @Email)
        BEGIN
            INSERT  INTO AlegrarioAppSchema.Auth (
                [Email],
                [PasswordHash],
                [PasswordSalt]
            ) VALUES (
                @Email,
                @PasswordHash,
                @PasswordSalt
            )
        END
    ELSE
        BEGIN
            UPDATE AlegrarioAppSchema.Auth
                SET PasswordHash = @PasswordHash,
                    PasswordSalt = @PasswordSalt
                WHERE Email = @Email
        END

END
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spLoginConfirmation_Get
/*EXEC AlegrarioAppSchema.spLoginConfirmation_Get*/
    @Email NVARCHAR(50)
AS
BEGIN
    SELECT  [Auth].[PasswordHash],
            [Auth].[PasswordSalt] 
    FROM AlegrarioAppSchema.Auth AS Auth
        WHERE Auth.Email = @Email
END