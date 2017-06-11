USE [C30]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[File_SelectByPersonId]
		@PersonID int

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
  	WHERE	PersonID = @PersonID
END

/*----------TEST----------------

DECLARE @PersonID int = 8;

EXECUTE [dbo].[File_SelectByPersonId] @PersonID

*/


