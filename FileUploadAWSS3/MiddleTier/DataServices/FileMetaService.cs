using Aic.Data;
using Aic.Web.Domain;
using Aic.Web.Models.Requests.File;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Aic.Web.Services
{
    public class FileMetaService : BaseService
    {
	//grab the id of the current logged in user
        public static string userGuid = UserService.GetCurrentUserId();

	//store the Amazon file url into SQL database
        public int FileInsert(FileInsertRequest request)
        {
	//store the Amazon file url into SQL database
            int ID = 0;
            Guid CreatedBy = new Guid(userGuid);
            Guid ModifiedBy = CreatedBy;

            DataProvider.ExecuteNonQuery(GetConnection,
                "dbo.File_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonID", request.PersonID);
                    paramCollection.AddWithValue("@FileUrl", request.FileUrl);
                    paramCollection.AddWithValue("@FileTitle", request.FileTitle);
                    paramCollection.AddWithValue("@FileType", (int)request.FileType);
                    paramCollection.AddWithValue("@CreatedBy", CreatedBy);

                    SqlParameter p = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection paramCollection)
                {
                    int.TryParse(paramCollection["@ID"].Value.ToString(), out ID);
                });
            return ID;
        }

        public void FileDelete(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection,
                "dbo.File_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", id);
                });
        }

        public static UserFile FileGetById(int id)
        {
            UserFile row = null;

            DataProvider.ExecuteCmd(GetConnection,
                "dbo.File_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ID", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    UserFile f = new UserFile();
                    int startingIndex = 0;

                    f.ID = reader.GetSafeInt32(startingIndex++);
                    f.PersonID = reader.GetSafeInt32(startingIndex++);
                    f.FileUrl = reader.GetSafeString(startingIndex++);
                    f.FileTitle = reader.GetSafeString(startingIndex++);
                    f.FileType = reader.GetSafeInt32(startingIndex++);
                    f.CreatedBy = reader.GetSafeString(startingIndex++);
                    f.ModifiedBy = reader.GetSafeString(startingIndex++);
                    f.ModifiedDate = reader.GetSafeDateTime(startingIndex++);

                    if (row == null)
                    {
                        row = f;
                    }
                    
                });
            return row;
        }

        public static List<UserFile> FileGetByComplexUserId()
        {
            Guid ComplexID = new Guid(userGuid);
            List<UserFile> list = null;

            DataProvider.ExecuteCmd(GetConnection,
                "dbo.File_GetByComplexUserId",
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ComplexID", ComplexID);

                }, map: delegate (IDataReader reader, short set)
                {
                    UserFile f = new UserFile();
                    int startingIndex = 0;

                    f.ID = reader.GetSafeInt32(startingIndex++);
                    f.PersonID = reader.GetSafeInt32(startingIndex++);
                    f.FileUrl = reader.GetSafeString(startingIndex++);
                    f.FileTitle = reader.GetSafeString(startingIndex++);
                    f.FileType = reader.GetSafeInt32(startingIndex++);


                    if (list == null)
                    {
                        list = new List<UserFile>();
                    }
                    list.Add(f);
                });
            return list;
        }

        public static List<UserFile> FileGetByPersonId(int personId)
        {
            List<UserFile> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.File_SelectByPersonId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PersonID", personId);
                }, map: delegate (IDataReader reader, short set)
                {
                    UserFile f = new UserFile();
                    int startingIndex = 0;

                    f.ID = reader.GetSafeInt32(startingIndex++);
                    f.PersonID = reader.GetSafeInt32(startingIndex++);
                    f.FileUrl = reader.GetSafeString(startingIndex++);
                    f.FileTitle = reader.GetSafeString(startingIndex++);
                    f.FileType = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<UserFile>();
                    }

                    list.Add(f);
                });

            return list;
        }
    }
}