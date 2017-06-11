using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace Aic.Web.Services
{
    public class RecaptchaService
    {
        //The two properties below are what Google returns in the api call (listed in the recaptcha documentation)
        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; }

        //method to send api call to google and receive response payload.
        public static bool Validate(string response, string key)
        {
            if (string.IsNullOrEmpty(response) || string.IsNullOrEmpty(key))
            {
                return false;
            }
            //create new instance of the object/class for access to methods that send and receive data from a URI
            var client = new System.Net.WebClient();
            var secret = key;

            //google recaptcha dev documentation states to send a POST request to the below url with the required parameters of 
            //the secret key provided and the response from the 'g-recapthca-response' in the front end
            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            // ^ The DownloadString Method downloads the requested resource as a String. The resource to download may be specified as 
            //either String containing the URI or a Uri.

            //use JsonConvert/ serializer in order to have faster performance (Javascript (de)serializer is slower, but method is shown below, commented out)
            var reCaptcha = JsonConvert.DeserializeObject<RecaptchaService>(googleReply);

            /*
            //JavascriptSerializer() provides serialization and deserialization functionality for AJAX-enabled applications.
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //Deserialize method Converts the specified JSON string to an object of type <T> (model).
            var reCaptcha = serializer.Deserialize<RecaptchaService>(googleReply);
            */

            //return Success property bool value
            return reCaptcha.Success;
        }
    }
}