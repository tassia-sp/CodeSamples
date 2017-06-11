USE [C30]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[File_SelectById]
		@ID int

AS

BEGIN
	SELECT	[ID]
      		,[PersonID]
      		,[FileUrl]
      		,[FileTitle]
	  	,[FileType]
      		,[CreatedBy]
      		,[ModifiedBy]
      		,[ModifiedDate]

  	FROM	[dbo].[File]

  	WHERE	ID = @ID
END

/*----------TEST----------------
DECLARE	@PersonId int = 1;

EXECUTE [dbo].[File_SelectByPersonId] @PersonId

*/


