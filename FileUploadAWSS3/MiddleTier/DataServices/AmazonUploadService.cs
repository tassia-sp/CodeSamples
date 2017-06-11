using Amazon.S3;
using Amazon.S3.Model;
using Aic.Web.Models.Requests;
using Aic.Web.Models.Requests.Files;
using System;
using System.Configuration;
using System.Net;

namespace Aic.Web.Services
{
    public class AmazonUploadService : BaseService, IUploadService
    {
        public static string bucketName = "Aic/C30";

        public bool UploadFileFromFolder(AmazonAddRequest file)
        {
            bool retVal = false;

            var accessKey = ConfigurationManager.AppSettings["AWSAccessKey"];// Get access key from a secure store
            var secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];// Get secret key from a secure store

            string fileName = file.keyName;
            string localFile = file.localFilePath;
            string message = string.Empty;

            using (AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USWest2)) 
            try
            {
                PutObjectRequest req = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = fileName,
                    FilePath = localFile
                };


                PutObjectResponse resp = client.PutObject(req);
                retVal = resp.HttpStatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {

                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    message = "Check the provided AWS Credentials. \n For service sign up go to http://aws.amazon.com/s3";
                    //give back the error to main()
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    message = String.Format("Error occurred. Message:'{0}' when writing an object", amazonS3Exception.Message);
                    //throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
            return retVal;

        }

        public bool DeleteFileFromS3(AmazonDeleteRequest payload)
        {
            var accessKey = ConfigurationManager.AppSettings["AWSAccessKey"];// Get access key from a secure store
            var secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];// Get secret key from a secure store

            string filePath = payload.FileUrl;
            string message = string.Empty;
            bool retVal = false;

            using (AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USWest2))
            {
                try
                {
                    DeleteObjectRequest req = new DeleteObjectRequest()
                    {
                        BucketName = bucketName,
                        Key = filePath

                    };
                    DeleteObjectResponse resp = client.DeleteObject(req);
                    if (resp.HttpStatusCode == HttpStatusCode.NoContent)
                    {
                        // success code 
                        retVal = true;
                    }
                    else
                    {
                        retVal = false;// unsuccessful
                    }
                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        message = "Check the provided AWS Credentials. \n For service sign up go to http://aws.amazon.com/s3";
                        //give back the error to main()
                        throw new Exception("Check the provided AWS Credentials.");
                    }
                    else
                    {
                        message = String.Format("Error occurred. Message:'{0}' when writing an object", amazonS3Exception.Message);
                        //throw new Exception("Error occurred: " + amazonS3Exception.Message);
                    }
                }
            }
            return retVal;
        }
    }
}