ALTER proc [dbo].[ReferralRequestInternal_SelectPendingReferralByIds]
--to grab information for the referrer to see the information about the candidate and job
--displayed on the referral form
				@ReferrerId int
				,@CandidateId int
				,@jobId int

AS

BEGIN
		SELECT	r.ReferrerId
				,r.CandidateId
				,r.JobId 
				,j.CompanyName
				,j.Title
				,p.FirstName as CandidateName
				,p.ComplexUserId as CandidateGuid
		FROM	dbo.ReferralRequestInternal r
		join dbo.Job j on r.JobId = j.Id
		join dbo.People p on r.CandidateId = p.ID
		WHERE	ReferrerId = @ReferrerId AND  CandidateId = @CandidateId AND JobId = @JobId --AND p.ID = @ReferrerId

END

/*----------TEST-------
Declare @ReferrerId int = 4
		,@CandidateId int = 34
		,@jobId int = 23 
Execute dbo.ReferralRequestInternal_SelectPendingReferralByIds
		@ReferrerId
		,@CandidateId
		,@JobId

select * from dbo.referralrequestinternal

select * from dbo.people
*/
