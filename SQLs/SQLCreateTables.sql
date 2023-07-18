USE Alegrario
GO

CREATE SCHEMA AlegrarioAppSchema
GO

CREATE TABLE AlegrarioAppSchema.Users
(
    UserId INT IDENTITY(1, 1) PRIMARY KEY
    , FirstName NVARCHAR(50)
    , LastName NVARCHAR(50)
    , Email NVARCHAR(50)
    , Gender NVARCHAR(50)
    , Active BIT
);
GO

CREATE NONCLUSTERED INDEX fix_Users_Active
    ON AlegrarioAppSchema.Users (Active)
    INCLUDE (Email, FirstName, LastName, Gender)
    WHERE active = 1;

CREATE TABLE AlegrarioAppSchema.Auth(
	Email NVARCHAR(50) PRIMARY KEY,
	PasswordHash VARBINARY(MAX),
	PasswordSalt VARBINARY(MAX)
)
GO



CREATE TABLE AlegrarioAppSchema.DayCells (
    CellDay DATE,
    UserId INT,
    PRIMARY KEY (CellDay, UserId),
    FOREIGN KEY (UserId) REFERENCES AlegrarioAppSchema.Users(UserId)
);
GO


CREATE TABLE AlegrarioAppSchema.Emotions (
    HourId INT ,
    CellDay DATE,
    UserId INT,
    EmotionValue INT,
    Comment NVARCHAR(50),
    Score INT,
    PRIMARY KEY (CellDay, UserId, HourId),
    FOREIGN KEY (CellDay, UserId) REFERENCES AlegrarioAppSchema.DayCells(CellDay,UserId)
);
GO

SELECT * FROM AlegrarioAppSchema.Users AS Users
LEFT JOIN AlegrarioAppSchema.DayCells AS DayCell
ON Users.UserId = DayCell.UserId
SELECT * FROM AlegrarioAppSchema.Auth

SELECT * FROM AlegrarioAppSchema.DayCells

SELECT * FROM AlegrarioAppSchema.Emotions

INSERT INTO AlegrarioAppSchema.DayCells (CellDay, UserId)
VALUES ('2023-08-23', 3);



EXEC AlegrarioAppSchema.spUser_Get @UserId = 2;



INSERT INTO AlegrarioAppSchema.Emotions (HourId, CellDay, UserId, EmotionValue, Comment, Score)
VALUES
    (0, '2023-07-18', 2, 1, 'Feeling happy', 10),
    (23, '2023-08-17', 2, 2, 'Feeling neutral', 5),
    (0, '2023-07-22', 3, 4, 'Feeling sad', 8),
    (12, '2023-07-22', 3, 3, 'Feeling angry', 10),
    (4, '2023-07-22', 3, 3, 'Feeling angry', 5),
    (4, '2023-07-23', 3, 2, 'Feeling neutral', 5),
    (3, '2023-08-23', 3, 1, 'Feeling happy', 3);