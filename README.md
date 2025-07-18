# crudwithphoto
--crudwithphoto


--delete procedure 

USE [Assignment_DB]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 7/19/2025 1:44:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DeleteUser]
    @UserId INT
AS
BEGIN
    -- Optional: Insert into a trigger table for backup/audit
    INSERT INTO TriggerTable (UserId, ActionDate)
    SELECT UserId, GETDATE()
    FROM Users WHERE UserId = @UserId;

    -- Delete the user from Users table
    DELETE FROM Users WHERE UserId = @UserId
END




--insert procedure 

USE [Assignment_DB]
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 7/19/2025 1:45:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertUser] 
    @FullName NVARCHAR(255),
    @MobileNo NVARCHAR(10),
    @Email NVARCHAR(255),
    @DateOfBirth DATE,
    @Age INT,
    @State NVARCHAR(100),
    @District NVARCHAR(100),
    @PhotoPath NVARCHAR(255),
    @CreatedBy NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users (FullName, MobileNo, Email, DateOfBirth, Age, State, District, PhotoPath, CreatedOn, CreatedBy)
    VALUES (@FullName, @MobileNo, @Email, @DateOfBirth, @Age, @State, @District, @PhotoPath, GETDATE(), @CreatedBy)
END



--update procedure 

USE [Assignment_DB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 7/19/2025 1:45:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateUser]
    @UserId INT,
    @FullName NVARCHAR(100),
    @MobileNo NVARCHAR(15),
    @Email NVARCHAR(100),
    @DateOfBirth DATE,
    @Age INT,
    @State NVARCHAR(50),
    @District NVARCHAR(50),
    @PhotoPath NVARCHAR(255),
    @CreatedBy NVARCHAR(50)
AS
BEGIN
    UPDATE Users
    SET FullName = @FullName,
        MobileNo = @MobileNo,
        Email = @Email,
        DateOfBirth = @DateOfBirth,
        Age = @Age,
        State = @State,
        District = @District,
        PhotoPath = @PhotoPath,
        CreatedBy = @CreatedBy,
        CreatedOn = GETDATE()
    WHERE UserId = @UserId
END



