USE [C30]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[File](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[FileUrl] [nvarchar](150) NOT NULL,
	[FileTitle] [nvarchar](150) NOT NULL,
	[FileType] [int] NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[ModifiedBy] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_FileType] FOREIGN KEY([FileType])
REFERENCES [dbo].[FileType] ([ID])
GO

ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_FileType]
GO

ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_People] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([ID])
GO

ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_People]
GO




