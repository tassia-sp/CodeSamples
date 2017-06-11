USE [C30]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[File_GetByComplexUserId]
		@ComplexID uniqueidentifier

AS

BEGIN
	SELECT	f.ID
	  	,f.PersonID
      		,f.FileUrl
      		,f.FileTitle
	  	,f.FileType
		from [file] f join people p on f.personid = p.id
		where p.complexuserid= @ComplexID
END
