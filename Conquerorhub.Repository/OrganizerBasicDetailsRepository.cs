using Conquerorhub.Models;
using Conquerorhub.Request.Models;
using Conquerorhub.Request.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Repository
{
   public class OrganizerBasicDetailsRepository:RepositoryBase
    {
        public RequestResult<Sponsors> SaveUserDeatils(string sessionToken, Sponsors model)
        {
            string parameters = $"/OrganizerDetails/savesponsordetails?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model.sponsorSingleData, new HttpPostedFileConverter());
                return PostAndGetData<RequestResult<Sponsors>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
               
        public RequestResult<List<Sponsor1>> GetSponsorList(string sessionToken,Guid? userid)
        {
            var parameters = $"/OrganizerDetails/getsponsordetails/?sessionToken={sessionToken}&UserId={userid}";
            try
            {
                return GetAndParseData<RequestResult<List<Sponsor1>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<OrganizerAboutusModels> SaveOragnizerAboutUs(string sessionToken, OrganizerAboutusModels model)
        {
            string parameters = $"/OrganizerDetails/saveorganizeraboutus?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<OrganizerAboutusModels>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OrganizerAboutusModels> GetOrganizerAboutUs(string sessionToken,Guid? userid)
        {
            var parameters = $"/OrganizerDetails/getorganizeraboutus/?sessionToken={sessionToken}&UserId={userid}";
            try
            {
                return GetAndParseData<RequestResult<OrganizerAboutusModels>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

    }
    public class HttpPostedFileConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stream = (Stream)value;
            using (var sr = new BinaryReader(stream))
            {
                var buffer = sr.ReadBytes((int)stream.Length);
                writer.WriteValue(Convert.ToBase64String(buffer));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Stream));
        }
    }
}
