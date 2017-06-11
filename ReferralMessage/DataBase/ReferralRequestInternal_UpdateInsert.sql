ALTER proc [dbo].[ReferralRequestInternal_UpdateInsert]
				@ID int output
				,@ReferrerId int
				,@CandidateId int
				,@JobId int
				,@Message nvarchar(max) = null
				,@Accepted bit
				,@Favorite bit = null
				,@Hidden bit = null
				,@UserNotified bit = null
AS

IF EXISTS (SELECT * FROM dbo.ReferralRequestInternal WHERE (CandidateId = @CandidateId) AND (JobId = @JobId) AND (ReferrerId = @ReferrerId))

BEGIN
		UPDATE	dbo.referralrequestinternal
		   SET	
				[Message] = @Message
				,[Accepted] = @Accepted
				,[Favorite] = @Favorite
				,[Hidden] = @Hidden
				,[UserNotified] =@UserNotified
				,DateCreated = getutcdate()
		 WHERE	[CandidateId] = @CandidateId AND [JobId] = @JobId AND ReferrerId = @ReferrerId
END
ELSE
BEGIN
	INSERT INTO	[dbo].[ReferralRequestInternal]
				([ReferrerId]
				,[CandidateId]
				,[JobId]
				,[Accepted])

		 VALUES	(@ReferrerId
				,@CandidateId
				,@JobId
				,@Accepted)
			SET @Id = SCOPE_IDENTITY()
END

/*----TEST CODE----
Declare 		@ID int
				,@ReferrerId int
				,@CandidateId int = 10
				,@JobId int = 25
				,@Message nvarchar(max) = 'David would be terrible at this position because he is lazy'
				,@Accepted bit = 0
Execute dbo.ReferralRequestInternal_UpdateInsert
				@ID
				,@ReferrerId
				,@CandidateId
				,@JobId
				,@Message
				,@Accepted
				select * from dbo.ReferralRequestInternal where jobid = 25
*/