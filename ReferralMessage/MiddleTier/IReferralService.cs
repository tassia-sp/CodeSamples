using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using System.Collections.Generic;

namespace Aic.Web.Services
{
    public interface IReferralService
    {
        int ReferralUpdateInsert(ReferralRequest payload);
        List<ReferralPending> GetPendingReferral(int referrerId, int candidateId, int jobId);
        List<ReferralsJob> GetMyReferrals(int ID);
    }
}