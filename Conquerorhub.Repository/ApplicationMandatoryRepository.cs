using Conquerorhub.Models;
using Conquerorhub.Request.Models;
using Conquerorhub.Request.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Repository
{
   public class ApplicationMandatoryRepository: RepositoryBase
    {
        public RequestResult<SessionTokenModel> SaveSessionToken(SessionTokenModel userSession)
        {
            string parameters = $"/applicationmandatory/savesessiontoken?sessionToken=";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(userSession);
                //string parameters = $"/usersecurity/saveuserdetails?sessionToken={sessionToken}&Userdetail={serilizedData}";          
                return PostAndGetData<RequestResult<SessionTokenModel>>(null, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<AspnetUsersModel>> Getuserlist(string sessionToken)
        {
            var parameters = $"/applicationmandatory/getuserlist/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<List<AspnetUsersModel>>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<LastLoginModel> GetLastLogin(string sessionToken)
        {
            var parameters = $"/applicationmandatory/getuserlist/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<LastLoginModel>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
