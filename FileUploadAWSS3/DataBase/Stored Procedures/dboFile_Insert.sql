USE [C30]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
		     			 
ALTER Proc [dbo].[File_Insert]		 
			 @PersonID int
             		 ,@FileUrl nvarchar(150)
             		 ,@FileTitle nvarchar(50)
			 ,@FileType int
             		 ,@CreatedBy nvarchar(128)
			 ,@ID int OUTPUT

AS

BEGIN
	DECLARE 	@Today DATETIME = GETUTCDATE()

	INSERT INTO	[dbo].[File]
           		([PersonID]
           		,[FileUrl]
           		,[FileTitle]
		   	,[FileType]
           		,[CreatedBy]
           		,[ModifiedDate])

    	 VALUES		(@PersonID
             		,@FileUrl
             		,@FileTitle
			,@FileType
             		,@CreatedBy
             		,@Today)

		SET	@ID = SCOPE_IDENTITY()

END

/*---------------------TEST--------------------

DECLARE @TODAY DATETIME = GETUTCDATE();

DECLARE	@PersonID int = 1
        ,@FileUrl nvarchar(150) = 'www.url.com'
        ,@FileTitle nvarchar(50) = 'TITLE'
        ,@CreatedBy nvarchar(128) = NEWID()
	,@ID int


EXECUTE [dbo].[File_Insert]
		@PersonID
             	,@FileUrl
             	,@FileTitle
             	,@CreatedBy
		,@ID OUTPUT

*/
