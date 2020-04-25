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
  public  class EventsRepository: RepositoryBase
    {

        public RequestResult<AboutEvent> SaveRegistrationABoutEvent(string sessionToken, EventRegistrationfromOrganizerModel userSession)
        {
            string parameters = $"/Events/saveOregisteredAboutevent?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(userSession.aboutEvent);

                return PostAndGetData<RequestResult<AboutEvent>>(null, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<AboutEvent>> GetRegistrationABoutEvent(string sessionToken,Guid? Userid)
        {
            try
            {
                var parameters = $"/Events/getOregisteredAboutevent/?sessionToken={sessionToken}&Userid={Userid}";

                return GetAndParseData<RequestResult<List<AboutEvent>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<AboutEvent>> GetAllEvents(string sessionToken)
        {
            try
            {
                var parameters = $"/Events/getallevents/?sessionToken={sessionToken}";

                return GetAndParseData<RequestResult<List<AboutEvent>>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<AwardsAndRewards> SaveAwardAndReward(string sessionToken, EventRegistrationfromOrganizerModel userSession)
        {
            string parameters = $"/Events/saveOAwardsandRewards?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(userSession.awardReward);
                //string parameters = $"/usersecurity/saveuserdetails?sessionToken={sessionToken}&Userdetail={serilizedData}";          
                return PostAndGetData<RequestResult<AwardsAndRewards>>(null, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<AwardsAndRewards>> GetAwardAndReward(string sessionToken,Guid? Userid)
        {
            var parameters = $"/Events/getOAwardsandRewards/?sessionToken={sessionToken}&Userid={Userid}";
            try
            {
                return GetAndParseData<RequestResult<List<AwardsAndRewards>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<AboutParticipants> SaveRegistrationAboutParticipant(string sessionToken, EventRegistrationfromOrganizerModel userSession)
        {
            string parameters = $"/Events/SaveOregisteredAboutparticipant?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(userSession.aboutParticipants);
                //string parameters = $"/usersecurity/saveuserdetails?sessionToken={sessionToken}&Userdetail={serilizedData}";          
                return PostAndGetData<RequestResult<AboutParticipants>>(null, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<AboutParticipants>> GetRegistrationAboutParticipant(string sessionToken,Guid? Userid)
        {
            var parameters = $"/Events/getOregisteredAboutparticipant/?sessionToken={sessionToken}&Userid={Userid}";
            try
            {

                return GetAndParseData<RequestResult<List<AboutParticipants>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public RequestResult<List<SubTypeEventsModel>> GetEventSubtype(string sessionToken)
        {
            var parameters = $"/Events/geteventsubtype/?sessionToken={sessionToken}";
            try
            {
                return GetAndParseData<RequestResult<List<SubTypeEventsModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<ImportantDates> SaveImportantDatesofRegistration(string sessionToken, EventRegistrationfromOrganizerModel userSession)
        {
            string parameters = $"/Events/saveOimportantdates?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(userSession.importantDates);
                      
                return PostAndGetData<RequestResult<ImportantDates>>(null, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<ImportantDates>> GetImportantDatesofRegistration(string sessionToken,Guid? Userid)
        {
            var parameters = $"/Events/getOimportantdates/?sessionToken={sessionToken}&Userid={Userid}";
            try
            {

                return GetAndParseData<RequestResult<List<ImportantDates>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
