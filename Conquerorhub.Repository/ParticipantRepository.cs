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
   public class ParticipantRepository:RepositoryBase
    {
        public RequestResult<ParticipationRegistrationModel> SaveParticipantsregistration(string sessionToken, ParticipationRegistrationModel model)
        {
            try
            {
                string parameters = $"/participants/saveparticipantsregistration?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<ParticipationRegistrationModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OTPVerificationModel> SaveOTP(string sessionToken, OTPVerificationModel model)
        {
            try
            {
                string parameters = $"/Events/saveotp?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<OTPVerificationModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OngoingEventparicipantslist> SaveOngoingEventData(string sessionToken, OngoingEventparicipantslist model)
        {
            try
            {
                string parameters = $"/Events/saveongoingeventdata?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<OngoingEventparicipantslist>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OTPVerificationModel> DeleteOTP(string sessionToken, OTPVerificationModel model)
        {
            try
            {
                string parameters = $"/Events/deleteotp?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<OTPVerificationModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<ParticipationRegistrationModel>> GetParticipantsregistration(string sessionToken, Guid EventId)
        {
            try
            {
                var parameters = $"/participants/getparticipantsregistration/?sessionToken={sessionToken}&eventId={EventId}";

                return GetAndParseData<RequestResult<List<ParticipationRegistrationModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        //public RequestResult<List<OngoingEventparicipantslist>> OngoingEventParticipantList(string sessionToken, Guid EventId)
        //{
        //    var parameters = $"/Events/getOngoingEventList/?sessionToken={sessionToken}&Eventid={EventId}";


        //    return GetAndParseData<RequestResult<List<OngoingEventparicipantslist>>>(null, parameters);

        //}
        public RequestResult<List<OTPVerificationModel>> GetOTPlist(string sessionToken, Guid EventId)
        {
            try
            {
                var parameters = $"/Events/getotplist/?sessionToken={sessionToken}";


                return GetAndParseData<RequestResult<List<OTPVerificationModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<ParticipationRegistrationModel> UpdatePArticipantStatus(string sessionToken, ParticipationRegistrationModel model)
        {
            try
            {
                string parameters = $"/Events/UpdateParticipantStatus?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<ParticipationRegistrationModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GalleryViewModel> SaveParticipantPhotoGallery(string sessionToken, GalleryViewModel model)
        {
            string parameters = $"/participants/saveparticipantsphotogallery?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model.GalleryData, new HttpPostedFileConverter());

                return PostAndGetData<RequestResult<GalleryViewModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<GalleryModel>> GetParticipantPhotoGallery(string sessionToken, Guid? userid)
        {
            try
            {
                var parameters = $"/participants/getparticipantphotogallery/?sessionToken={sessionToken}&userid={userid}";

                return GetAndParseData<RequestResult<List<GalleryModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<ParticipantAboutModel>> GetParticipantAboutdetails(string sessionToken, Guid? userid)
        {
            try
            {
                var parameters = $"/participants/getparticipantAbout/?sessionToken={sessionToken}&userid={userid}";

                return GetAndParseData<RequestResult<List<ParticipantAboutModel>>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //public RequestResult<List<ParticipantAboutModel>> GetParticipantcontact(string sessionToken, Guid? userid)
        //{
        //    var parameters = $"/participants/getparticipantphotogallery/?sessionToken={sessionToken}&userid={userid}";

        //    return GetAndParseData<RequestResult<List<ParticipantAboutModel>>>(null, parameters);

        //}
        //public RequestResult<List<ParticipantAboutModel>> GetParticipantEducation(string sessionToken, Guid? userid)
        //{
        //    var parameters = $"/participants/getparticipantphotogallery/?sessionToken={sessionToken}&userid={userid}";

        //    return GetAndParseData<RequestResult<List<ParticipantAboutModel>>>(null, parameters);

        //}
        public RequestResult<ParticipantAboutModel> SaveParticipantbasicdetails(string sessionToken, ParticipantAboutModel model)
        {
            try
            {
                string parameters = $"/participants/saveparticipantsAbout?sessionToken={sessionToken}";

                var serilizedData = JsonConvert.SerializeObject(model);


                return PostAndGetData<RequestResult<ParticipantAboutModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ParticipantAboutModel> SaveParticipantcontact(string sessionToken, ParticipantAboutModel model)
        {
            try
            {
                string parameters = $"/participants/saveparticipantsAbout?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<ParticipantAboutModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ParticipantAboutModel> SaveParticipantEducation(string sessionToken, ParticipantAboutModel model)
        {
            try
            {
                string parameters = $"/participants/saveparticipantsAbout?sessionToken={sessionToken}";
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<ParticipantAboutModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
