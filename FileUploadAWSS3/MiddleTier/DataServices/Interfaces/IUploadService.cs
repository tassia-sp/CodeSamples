using Aic.Web.Models.Requests;
using Aic.Web.Models.Requests.Files;

namespace Aic.Web.Services
{
    public interface IUploadService
    {
        bool DeleteFileFromS3(AmazonDeleteRequest payload);
        bool UploadFileFromFolder(AmazonAddRequest file);
    }
}