USE [C30]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[File_Delete]
	@ID int

AS

BEGIN

	DELETE FROM [dbo].[File]
      	WHERE @ID = ID
END

/*----------TEST-----------

	DECLARE @ID int = 8

	EXECUTE dbo.File_Delete @ID

	Select *
	FROM [File]

*/


