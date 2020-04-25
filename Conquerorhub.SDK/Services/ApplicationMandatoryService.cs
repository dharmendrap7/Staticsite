using Conquerorhub.Models;
using Conquerorhub.Repository;
using Conquerorhub.Request.Models;
using Conquerorhub.SDK.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Conquerorhub.SDK.Services
{
    public class ApplicationMandatoryService
    {
        private readonly ApplicationMandatoryRepository _repository;

        public ApplicationMandatoryService()
        {
            _repository = new ApplicationMandatoryRepository();
        }
        public RequestResult<SessionTokenModel> SaveUserSession()
        {
            SessionTokenModel model = new SessionTokenModel();
            model.SessionToken1 = Guid.NewGuid().ToString().Replace("-", string.Empty);
            System.Web.HttpContext.Current.Session["SessionToken"] = model.SessionToken1;
            model.CreatedDateandTime = DateTime.Now;
            model.ExpiryDateandTime = DateTime.Now.AddDays(1);
            model.UserId = System.Web.HttpContext.Current.Session["UserId"] as string;
            model.id = Guid.NewGuid();
           // return 
               var sessionModel= _repository.SaveSessionToken(model);// 
            return sessionModel;
        }
        public RequestResult<List<AspnetUsersModel>> Getuserlist(string sessionToken)
        {
            try
            {
                return _repository.Getuserlist(sessionToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<LastLoginModel> GetLastLogin(string sessionToken)
        {
            try
            {
                return _repository.GetLastLogin(sessionToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<LastLoginModel> GetLastLogindb(string UserId)
        {


            using (var db = new ConquerorHubEntities())
            {
             
                try
                {
                    var result = (from details in db.CH_LastLogin
                                  where details.UserId == UserId

                                  select new LastLoginModel()
                                  {
                                      Id = details.Id,
                                      UserId = details.UserId,
                                      ExpiryDateTime = details.ExpiryDateTime,
                                      CreatedDateandTime = details.CreatedDateandTime,
                                      SessionToken = details.SessionToken,

                                  }).FirstOrDefault();
                    return await Task.FromResult(result);
                }
                catch (Exception)
                {
                    throw new Exception("Error getting sponsors");
                }




            }
        }
    }
}
