using Aic.Web.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aic.Web.Models.Requests.File
{
    public class FileInsertRequest
    {
        [Required]
        public int PersonID { get; set; }
        [Required]
        public string FileUrl { get; set; }
        [Required]
        public string FileTitle { get; set; }
        [Required]
        public FileType FileType { get; set; }
       
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}