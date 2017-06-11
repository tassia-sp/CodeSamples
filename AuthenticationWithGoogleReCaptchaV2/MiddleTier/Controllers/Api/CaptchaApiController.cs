using Aic.Web.Models.Requests;
using Aic.Web.Models.Responses;
using Aic.Web.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aic.Web.Controllers.Api
{
    [RoutePrefix("api/captcha")]
    public class CaptchaApiController : ApiController
    {
        [Route(""), HttpPost]
        public HttpResponseMessage ValidateCaptcha(RecaptchaPostRequest model)
        {

            //check if the model is valid
            if (!ModelState.IsValid)
            {
		//add to error log
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.ValidateCaptcha";
                error.ErrorMessage = ModelState.ToString();
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                //if the model isn't valid, return a error response
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //instantiate the item response model
            ItemResponse<bool> response = new ItemResponse<bool>();

            //grab the values from the model
            string key = model.secret;
            string request = model.response;

            //send POST request via the service we made which returns a bool
            bool isCaptchaValid = RecaptchaService.Validate(request, key);

            //save the bool value in our response model
            response.Item = isCaptchaValid;

            //return the response
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
