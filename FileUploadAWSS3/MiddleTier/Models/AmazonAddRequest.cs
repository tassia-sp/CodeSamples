using System;
using System.Collections.Generic;
using System.Web;
using Aic.Web.Domain;

namespace Aic.Web.Models.Requests
{
    public class AmazonAddRequest
    {
        //local host full path with newFileName
        // SiteConfig.BaseUrl + "/Uploads/myFile_715e2505-3fbf-470a-a6ae-18cc7911efa9.pdf"
        public string urlFilePath { get; set; }

        //local folder path
        //"C:\\SF.Code\\C25\\Sabio.Web\\Uploads"
        public string rootFolder { get; set; }

        //local folder path with newFileName
        //EX" "C:\\SF.Code\\C25\\Sabio.Web\\Uploads\\myFile_715e2505-3fbf-470a-a6ae-18cc7911efa9.pdf"
        public string localFilePath { get; set; }

        //newFileName = Keyname
        //EX: "myFile_715e2505-3fbf-470a-a6ae-18cc7911efa9.pdf"
        public string keyName { get; set; }

        //Ex:"myFile"
        public string fileName { get; set; }

        //Ex:"jpg"
        public string extension { get; set; }

        //Ex: "myFile.jpg"
        public string fileNameWithExtension { get; set; }

        public FileType FileType { get; set; }

    }
}