USE Alegrario
GO



CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spUser_Get
/*EXEC AlegrarioAppSchema.spUser_Get  @Active=1 , @UserId=1005*/
    @UserId INT = NULL
    , @Active BIT = NULL
AS
BEGIN

    SELECT [Users].[UserId],
    [Users].[FirstName],
    [Users].[LastName],
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active]
    FROM AlegrarioAppSchema.Users AS Users
    WHERE Users.UserId = ISNULL(@UserId , Users.UserId) 
        AND ISNULL( Users.Active , 0) = COALESCE(@Active , Users.Active,0) 
END