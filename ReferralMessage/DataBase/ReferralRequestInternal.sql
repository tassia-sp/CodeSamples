CREATE TABLE [dbo].[ReferralRequestInternal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReferrerId] [int] NULL,
	[CandidateId] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Accepted] [bit] NOT NULL,
	[Favorite] [bit] NULL,
	[Hidden] [bit] NULL,
	[DateCreated] [date] NULL,
	[UserNotified] [bit] NULL)


