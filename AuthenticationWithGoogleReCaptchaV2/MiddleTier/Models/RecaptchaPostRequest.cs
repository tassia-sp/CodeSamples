using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Aic.Web.Models.Requests
{
    public class RecaptchaPostRequest
    {
        [Required]
        public string secret {
            get
            {
                return ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            }
        }
        [Required]
        public string response { get; set; }
    }
}