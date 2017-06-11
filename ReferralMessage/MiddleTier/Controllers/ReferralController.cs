using Aic.Web.Controllers.Attributes;
using Aic.Web.Models.Requests;
using Aic.Web.Models.ViewModels;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    [AnonAuthorize(Roles="User")]
    [RoutePrefix("referrals")]
    public class ReferralController : BaseController
    {
        [Route("sender/{candidateId:int}/job/{jobId:int}")]
        public ActionResult Message(int candidateId, int jobId)
        {
            ItemViewModel<ReferralRequest> model = new ItemViewModel<ReferralRequest>();

            model.Item = new ReferralRequest
            {
                CandidateId = candidateId,
                JobId = jobId
            };

            return View(model);
        }
    }
}