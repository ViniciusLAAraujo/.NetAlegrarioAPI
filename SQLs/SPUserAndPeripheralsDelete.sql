USE Alegrario
GO

CREATE OR ALTER PROCEDURE AlegrarioAppSchema.spUser_Delete
/*EXEC AlegrarioAppSchema.spUser_Delete @UserId = 1 */
    @UserId INT
AS
BEGIN
    DELETE FROM AlegrarioAppSchema.Users
        WHERE UserId = @UserId 

    DELETE FROM AlegrarioAppSchema.DayCells
        WHERE UserId = @UserId 
END