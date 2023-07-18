USE Alegrario
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spUser_Upsert
/*EXEC AlegrarioAppSchema.spUser_Upsert @FirstName='TESTINGCHANGE' , @LastName = 'Testing' , @Email='email.test', @Gender = 'Testgender',@Active=1 , @UserId=1*/
    @FirstName NVARCHAR (50),
    @LastName NVARCHAR (50),
    @Email    NVARCHAR (50),
    @Gender   NVARCHAR (50),
    @Active   BIT = 1,
    @UserId   INT = NULL
AS
BEGIN

    IF NOT EXISTS ( SELECT * FROM AlegrarioAppSchema.Users WHERE UserId = @UserId )
        BEGIN
        IF NOT EXISTS ( SELECT * FROM AlegrarioAppSchema.Users WHERE Email = @Email )
            BEGIN
                DECLARE @OutPutUserId INT

                INSERT INTO AlegrarioAppSchema.Users(
                    [FirstName],
                    [LastName],
                    [Email],
                    [Gender],
                    [Active]
                ) VALUES(
                    @FirstName,
                    @LastName,
                    @Email,
                    @Gender,
                    @Active         
                )

                SET @OutPutUserId = @@IDENTITY

            END
        END
    ELSE
        BEGIN

            UPDATE AlegrarioAppSchema.Users
                SET FirstName = @FirstName,
                    LastName =@LastName,
                    Email = @Email,
                    Gender = @Gender,
                    Active = @Active
            WHERE UserId = @UserId

                
        END
END