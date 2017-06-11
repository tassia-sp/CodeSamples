using Microsoft.AspNet.Identity;
using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using Aic.Web.Models.Requests.File;
using Aic.Web.Models.Requests.Files;
using Aic.Web.Services;
using System;
using System.Web;

namespace Aic.Web.Controllers.Api
{
    public abstract class FileUploadApiController : BaseApiController
    {
        protected IUploadService _uploadService = null;

        //dependency injection is placed in the FileApiController (subclass)

        private static string userGuid = HttpContext.Current.User.Identity.GetUserId();

        //instantiate the file meta service.
        FileMetaService fms = new FileMetaService();

        //POST file to Amazon server
        public int UploadFile(AmazonAddRequest saveFile, string title)
        {
            bool retVal = false;
            int id = 0;

            //instantiate the request model for the SQL server
            FileInsertRequest payload = new FileInsertRequest();

            // grab the current user id from the User Service
            int personId = UserService.UserSelect().PersonId;
            
            //hydrate the payload with information from the amazon object
            payload.PersonID = personId;
            payload.FileUrl = saveFile.keyName;
            payload.FileTitle = title.Replace("\"", "");
            payload.CreatedBy = userGuid;
            payload.ModifiedBy = payload.CreatedBy;
            payload.FileType = saveFile.FileType;


            //upload the amazon object to the amazon server
            retVal = _uploadService.UploadFileFromFolder(saveFile);

            if (retVal)
            {
                //if successful (true), send the payload object to the SQL server.
               id = fms.FileInsert(payload);
            };
            return id;
        }

        //DELETE file from Amazon server
        public bool DeleteFile(int id)
        {
            bool retVal = false;

            UserFile fileInfo = FileMetaService.FileGetById(id);
            AmazonDeleteRequest payload = new AmazonDeleteRequest();
            payload.Id = fileInfo.ID;
            payload.FileUrl = fileInfo.FileUrl;

            retVal = _uploadService.DeleteFileFromS3(payload);

            if (retVal)
            {
                fms.FileDelete(payload.Id);
            };
            return retVal;
        }

    }
}
