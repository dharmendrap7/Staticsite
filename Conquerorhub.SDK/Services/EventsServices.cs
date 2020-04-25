using Conquerorhub.Models;
using Conquerorhub.Repository;
using Conquerorhub.Request.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.SDK.Services
{
  public  class EventsServices
    {

        private readonly EventsRepository _repository;

        public EventsServices()
        {
            _repository = new EventsRepository();
        }

        public RequestResult<AboutEvent> SaveRegistrationAboutEvent(string sessionToken, EventRegistrationfromOrganizerModel model)
        {

            try
            {
                var result = _repository.SaveRegistrationABoutEvent(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<AboutEvent>> GetRegistrationAboutEvent(string sessionToken,Guid? Userid)
        {
            try
            {
                return _repository.GetRegistrationABoutEvent(sessionToken, Userid);
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
                return _repository.GetAllEvents(sessionToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<SubTypeEventsModel>> Geteventsubtype(string sessionToken)
        {
            try
            {
                return _repository.GetEventSubtype(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<AboutParticipants> SaveORegistrtionAboutParticipant(string sessionToken, EventRegistrationfromOrganizerModel model)
        {

            try
            {
                var result = _repository.SaveRegistrationAboutParticipant(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<AboutParticipants>> GetORegistrtionAboutParticipant(string sessionToken,Guid? Userid)
        {
            try
            {
                var result = _repository.GetRegistrationAboutParticipant(sessionToken, Userid);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ImportantDates> SaveImportantDateAndTime(string sessionToken, EventRegistrationfromOrganizerModel model)
        {

            try
            {
                var result = _repository.SaveImportantDatesofRegistration(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<ImportantDates>> GetImportantDateAndTime(string sessionToken,Guid? Userid)
        {
            try
            {
                return _repository.GetImportantDatesofRegistration(sessionToken, Userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<AwardsAndRewards> SaveAwardAndReward(string sessionToken, EventRegistrationfromOrganizerModel model)
        {
            try
            {
                var result = _repository.SaveAwardAndReward(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<AwardsAndRewards>> GetAwardAndReward(string sessionToken,Guid? Userid)
        {
            try
            {
                return _repository.GetAwardAndReward(sessionToken, Userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
