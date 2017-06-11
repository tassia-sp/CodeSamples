using Aic.Web.Classes.Extensions;
using Aic.Web.Domain;
using Aic.Web.Models.Requests;
using Aic.Web.Models.Responses;
using Aic.Web.Services;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Aic.Web.Controllers.Api
{
    [RoutePrefix("api/file")]

    public class FileApiController : FileUploadApiController
    {
        public FileApiController(IUploadService uploadService)
        {
            this._uploadService = uploadService;
        }

        [Route("upload"), HttpPost]
        public async Task<HttpResponseMessage> FilePost()
        {
            ItemResponse<int> response = new ItemResponse<int>();

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/assets/images/");
            var uploadFile = new MultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data. upload the file
                await Request.Content.ReadAsMultipartAsync(uploadFile);
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in uploadFile.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);

                    //instantiate the request object
                    AmazonAddRequest saveFile = new AmazonAddRequest();

                    //grab the custom title from user input to use in payload later in the FileUploadApiController.
                    string title = file.Headers.ContentDisposition.Name;

                    //hydrate the object
                    saveFile.rootFolder = ("/~assets/images");
                    saveFile.localFilePath = file.LocalFileName;
                    saveFile.fileName = System.IO.Path.GetFileNameWithoutExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    saveFile.extension = file.Headers.ContentType.MediaType.Split('/')[1];
                    //saveFile.extension = System.IO.Path.GetExtension(file.Headers.ContentType);
                    saveFile.keyName = saveFile.fileName + Guid.NewGuid() + saveFile.extension;
                    saveFile.urlFilePath = HttpContext.Current.Server.MapPath(saveFile.rootFolder + saveFile.keyName);
                    saveFile.fileNameWithExtension = saveFile.fileName + saveFile.extension;

                    //Check File type!
                    FileType UserFileType = CheckFileType.ReturnFiletype(saveFile.extension);
                    saveFile.FileType = UserFileType;
                    if (UserFileType == FileType.NoType)
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
                    }

                    //Send payload to FileUploadApiController
                    response.Item = this.UploadFile(saveFile, title);

                    if (!Request.Content.IsMimeMultipartContent("form-data"))
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
                    }
                }

                return Request.CreateResponse(response);

            }
            catch (System.Exception e)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.FilePost";
                error.ErrorMessage = e.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage FileDelete(int id)
        {
            try
            {
                if (this.DeleteFile(id))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.FileDelete";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route(""), HttpGet]
        public HttpResponseMessage FileGetByComplexUserId()
        {
            try
            {
                ItemsResponse<UserFile> response = new ItemsResponse<UserFile>();

                response.Items = FileMetaService.FileGetByComplexUserId();

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.FileGetByComplexUserId";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("{personId:int}"), HttpGet]
        public HttpResponseMessage FileGetByPersonId(int personId)
        {
            try
            {
                ItemsResponse<UserFile> response = new ItemsResponse<UserFile>();

                response.Items = FileMetaService.FileGetByPersonId(personId);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.GetAddresses";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [Route("getFile/{Id:int}"), HttpGet]
        public HttpResponseMessage FileGetByFileId(int Id)
        {
            try
            {
                ItemResponse<UserFile> response = new ItemResponse<UserFile>();

                response.Item = FileMetaService.FileGetById(Id);

                return Request.CreateResponse(response);
            }
            catch (Exception ex)
            {
                ErrorLogService svc = new ErrorLogService();
                ErrorLogAddRequest error = new ErrorLogAddRequest();
                error.ErrorFunction = "Sabio.Web.Controllers.Api.GetAddresses";
                error.ErrorMessage = ex.Message;
                error.UserId = UserService.UserSelect().PersonId;
                svc.ErrorLogInsert(error);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}