using Aic.Data;
using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Aic.Web.Services
{
    public class ReferralService : BaseService, IReferralService
    {
        public int ReferralUpdateInsert(ReferralRequest payload)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.ReferralRequestInternal_UpdateInsert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ReferrerId", payload.ReferrerId);
                    paramCollection.AddWithValue("@CandidateId", payload.CandidateId);
                    paramCollection.AddWithValue("@JobId", payload.JobId);
                    paramCollection.AddWithValue("@Message", payload.Message);
                    //If Accepted is not equal to true, must set accepted to false so that it 
                    //is in pending status      
                    if (payload.Accepted != true)
                    {
                        payload.Accepted = false;
                    };
                    paramCollection.AddWithValue("Accepted", payload.Accepted);
                    paramCollection.AddWithValue("@Favorite", payload.Favorite);
                    paramCollection.AddWithValue("@Hidden", payload.Hidden);
                    paramCollection.AddWithValue("@UserNotified", payload.UserNotified);

                    SqlParameter c = new SqlParameter("@Id", SqlDbType.Int);
                    c.Direction = ParameterDirection.Output;
                    paramCollection.Add(c);
                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }

        public List<ReferralPending> GetPendingReferral(int referrerId, int candidateId, int jobId)
        {
            List<ReferralPending> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.ReferralRequestInternal_SelectPendingReferralByIds"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@ReferrerId", referrerId);
                     paramCollection.AddWithValue("@CandidateId", candidateId);
                     paramCollection.AddWithValue("@JobId", jobId);

                 }, map: delegate (IDataReader reader, short set)
                 {
                     ReferralPending r = new ReferralPending();
                     int startingOrdinal = 0;
                     r.ReferrerId = reader.GetSafeInt32(startingOrdinal++);
                     r.CandidateId = reader.GetSafeInt32(startingOrdinal++);
                     r.JobId = reader.GetSafeInt32(startingOrdinal++);
                     r.CompanyName = reader.GetSafeString(startingOrdinal++);
                     r.Title = reader.GetSafeString(startingOrdinal++);
                     r.CandidateName = reader.GetSafeString(startingOrdinal++);
                     r.CandidateGuid = reader.GetSafeString(startingOrdinal++);

                     if (list == null)
                     {
                         list = new List<ReferralPending>();
                     }
                     list.Add(r);
                 });
            return list;
        }

        public List<ReferralsJob> GetMyReferrals(int ID)
        {
            List<ReferralsJob> list = new List<ReferralsJob>();
            Dictionary<int, ReferralsJob> referralsDictionary = new Dictionary<int, ReferralsJob>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Profile_PendingReferrals"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonId", ID);

                }, map: delegate (IDataReader reader, short set)
                {
                    ReferralsJob referralsJob = new ReferralsJob();
                    switch (set)
                    {
                        case 0:
                            ReferralsJob r = new ReferralsJob();

                            int startingOrdinal = 0;

                            r.ID = reader.GetSafeInt32(startingOrdinal++);
                            r.Title = reader.GetSafeString(startingOrdinal++);
                            r.Description = reader.GetSafeString(startingOrdinal++);
                            r.CompanyName = reader.GetSafeString(startingOrdinal++);
                            r.CompanyLocation = reader.GetSafeString(startingOrdinal++);
                            r.Type = reader.GetSafeString(startingOrdinal++);
                            r.Salary = reader.GetSafeInt32(startingOrdinal++);
                            r.ReferralsPerson = new List<ReferralsPerson>();
                            list.Add(r);
                            referralsDictionary.Add(r.ID, r);
                            break;
                        case 1:
                            ReferralsPerson rp = new ReferralsPerson();

                            int rpStartingOrdinal = 0;

                            rp.ID = reader.GetSafeInt32(rpStartingOrdinal++);
                            rp.FileUrl = reader.GetSafeString(rpStartingOrdinal++);
                            rp.ReferrerId = reader.GetSafeInt32(rpStartingOrdinal++);
                            rp.Accepted = reader.GetSafeBool(rpStartingOrdinal++);
                            rp.FirstName = reader.GetSafeString(rpStartingOrdinal++);
                            rp.LastName = reader.GetSafeString(rpStartingOrdinal++);
                            referralsDictionary[rp.ID].ReferralsPerson.Add(rp);
                            break;
                    }
                });
            return list;
        }
    }
}