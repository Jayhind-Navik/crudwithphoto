# crudwithphoto
crudwithphoto


delete procedure 

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
