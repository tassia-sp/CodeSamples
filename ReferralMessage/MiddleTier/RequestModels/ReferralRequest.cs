using System;

namespace Aic.Web.Models.Requests
{
    public class ReferralRequest
    {
        public int? ReferrerId { get; set; }
        public string ReferrerGuid { get; set; }
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public string Message { get; set; }
        public bool Accepted { get;  set; }
        public bool? Favorite { get; set; }
        public bool? Hidden { get; set; }
        public string CandidateGuid { get; set; }
        public bool UserNotified { get; set; }
    }
}