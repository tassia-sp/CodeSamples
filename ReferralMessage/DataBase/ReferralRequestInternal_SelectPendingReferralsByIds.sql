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
	JOIN	dbo.Job j ON r.JobId = j.Id
	JOIN	dbo.People p ON r.CandidateId = p.ID
	WHERE	ReferrerId = @ReferrerId AND  CandidateId = @CandidateId AND JobId = @JobId

END

/*----------TEST-------
DECLARE @ReferrerId int = 4
	,@CandidateId int = 34
	,@jobId int = 23 
EXECUTE dbo.ReferralRequestInternal_SelectPendingReferralByIds
	@ReferrerId
	,@CandidateId
	,@JobId

SELECT * FROM dbo.referralrequestinternal

SELECT * FROM dbo.people
*/
