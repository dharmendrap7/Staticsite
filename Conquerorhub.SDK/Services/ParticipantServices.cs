using Conquerorhub.Models;
using Conquerorhub.Repository;
using Conquerorhub.Request.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.SDK.Services
{
   public class ParticipantServices
    {

        private readonly ParticipantRepository _repository;

        public ParticipantServices()
        {
            _repository = new ParticipantRepository();
        }

        public RequestResult<ParticipationRegistrationModel> SaveParticipantsregistration(string sessionToken, ParticipationRegistrationModel model)
        {
            model.Id = Guid.NewGuid();
            try
            {
                var result = _repository.SaveParticipantsregistration(sessionToken, model);
                return result;
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
                var result = _repository.UpdatePArticipantStatus(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OTPVerificationModel> SaveOTp(string sessionToken, OTPVerificationModel model)
        {
            try
            {
                var result = _repository.SaveOTP(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OngoingEventparicipantslist> SaveongoingEventData(string sessionToken, OngoingEventparicipantslist model)
        {
            try
            {
                var result = _repository.SaveOngoingEventData(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OTPVerificationModel> DeleteOtp(string sessionToken, OTPVerificationModel model)
        {
            try
            {
                var result = _repository.DeleteOTP(sessionToken, model);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<ParticipationRegistrationModel>> GetParticipantsregistration(string sessionToken, Guid EventId)
        {try
            {
                return _repository.GetParticipantsregistration(sessionToken, EventId);
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
                return _repository.GetParticipantAboutdetails(sessionToken, userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //public RequestResult<List<OngoingEventparicipantslist>> Getongoingparticipantlist(string sessionToken, Guid EventId)
        //{
        //    return _repository.OngoingEventParticipantList(sessionToken,  EventId);
        //}
        public RequestResult<List<OTPVerificationModel>> Getotplist(string sessionToken, Guid EventId)
        {
            try
            {
                return _repository.GetOTPlist(sessionToken, EventId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GalleryViewModel> SavePhotoGallery(string sessionToken, GalleryViewModel model)
        {
            model.GalleryData.Id = Guid.NewGuid();
            try
            {
                if (model != null)
                {

                    MemoryStream target1 = new MemoryStream();
                    model.GalleryData.UploadedPost.InputStream.CopyTo(target1);
                    byte[] Photo = target1.ToArray();
                    model.GalleryData.ImageData = Photo;
                    target1.Close();

                }

                return _repository.SaveParticipantPhotoGallery(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<GalleryModel>> GetParticipantPhotoGallery(string sessionToken, Guid? UserId)
        {
            try
            {
                return _repository.GetParticipantPhotoGallery(sessionToken, UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ParticipantAboutModel> SaveParticipantbasicdetails(string sessionToken, ParticipantAboutModel model)
        {try
            {

                return _repository.SaveParticipantbasicdetails(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ParticipantAboutModel> SaveParticipantcontact(string sessionToken, ParticipantAboutModel model)
        {
            try
            {
                return _repository.SaveParticipantbasicdetails(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ParticipantAboutModel> SaveParticipanteducation(string sessionToken, ParticipantAboutModel model)
        {
            try
            {

                return _repository.SaveParticipantbasicdetails(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

