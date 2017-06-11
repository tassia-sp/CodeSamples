using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aic.Web.Models.Requests.Files
{
    public class AmazonDeleteRequest : AmazonAddRequest
    {
        public string FileUrl { get; set; }

        public int Id { get; set; }
    }
}