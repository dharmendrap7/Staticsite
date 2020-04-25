using Conquerorhub.Models;
using Conquerorhub.Repository;
using Conquerorhub.Request.Models;
using Conquerorhub.SDK.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Conquerorhub.SDK.Services
{
    public class OrganiserBasicDetailsServices
    {
        private readonly OrganizerBasicDetailsRepository _repository;

        public OrganiserBasicDetailsServices()
        {
            _repository = new OrganizerBasicDetailsRepository();
        }

        public RequestResult<Sponsors> SaveUserDeatils(string sessionToken, Sponsors model)
        {
            model.sponsorSingleData.Id = Guid.NewGuid();
            try
            {
                if (model != null)
                {

                    MemoryStream target1 = new MemoryStream();
                    model.sponsorSingleData.UploadedImage.InputStream.CopyTo(target1);
                    byte[] Photo = target1.ToArray();
                    model.sponsorSingleData.Image = Photo;
                    target1.Close();

                }



                return _repository.SaveUserDeatils(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<OrganizerAboutusModels> SaveOrganizerAboutUs(string sessionToken, OrganizerAboutusModels model)
        {
            model.Id = Guid.NewGuid();
            try
            {
                return _repository.SaveOragnizerAboutUs(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Guid> PostHome(CH_HomeViewModel model)
        {
          
                if (model != null)
                {
                    if (model.homeSingledata.Image1 != null)
                    {
                        MemoryStream target1 = new MemoryStream();
                        model.homeSingledata.Image1.InputStream.CopyTo(target1);
                        byte[] Photo = target1.ToArray();
                        model.homeSingledata.Image = Photo;
                        target1.Close();
                    }
                    using (var db = new ConquerorHubEntities())
                    {
                        var data = new CH_HomePageData()
                        {
                            Id = model.homeSingledata.Id,
                            UserId = model.homeSingledata.UserId,
                            DateTime = DateTime.UtcNow,
                            Postid = model.homeSingledata.Postid,
                            Image = model.homeSingledata.Image,
                            Video = model.homeSingledata.Video,
                            PostText=model.homeSingledata.PostText,
                            
                        };
                        try
                        {
                            db.CH_HomePageData.Add(data);
                          
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                    
                }
               
                }
            return await Task.FromResult(Guid.NewGuid());

        }
        public async Task<List<Home>> GetHomePage(string sessionToken)
        {

            using (var db = new ConquerorHubEntities())
            {
                var userId = db.SessionTokens.Where(a => a.SessionToken1 == sessionToken).SingleOrDefault().UserId;
                try
                {
                    var result = (from details in db.CH_HomePageData


                                  select new Home()
                                  {
                                      Id = details.Id,
                                     Image=details.Image,
                                     Video=details.Video,
                                     Postid=details.Postid,
                                     PostText=details.PostText,
                                      UserId = details.UserId,
                                      DateTime=details.DateTime


                                  }).ToList();
                    return  await Task.FromResult(result); 
                }
                catch (Exception)
                {
                    throw new Exception("Error getting image");
                }
            }

        }

        public async Task<List<ParticipationRegistrationModel>> GetParticipantData(string sessionToken)
        {
            using (var db = new ConquerorHubEntities())
            {
                var userId = db.SessionTokens.Where(a => a.SessionToken1 == sessionToken).SingleOrDefault().UserId;
                try
                {
                    var result = (from details in db.CH_ParticipantRegistration


                                  select new ParticipationRegistrationModel()
                                  {
                                      Data = details.Data,

                                      OrganizerId = details.OrganizerId,
                                      ParticipantId = details.ParticipantId,
                                      EventId = details.EventId,
                                      VideoId = details.ParticipantsPostId,
                                      Name = details.Name,
                                      Qualification = details.Qualification,
                                      CollegeorSchool = details.CollegeorSchool,
                                  


                                  }).ToList();
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        public string Imageget(byte[] Image)
        {
            Stream inputstream = new MemoryStream(Image, 0, Image.Length);
            MemoryStream memoryStream = inputstream as MemoryStream;
            if (memoryStream == null)
            {
                memoryStream = new MemoryStream();
                inputstream.CopyTo(memoryStream);
            }

            string imreBase64Data = Convert.ToBase64String(Image);
            string imgDataURL2 = string.Format("data:image2/png;base64,{0}", imreBase64Data);
            return imgDataURL2;
        }
        public async Task<List<GalleryModel>> GetParticipantGallerydata(string sessionToken)
        {
            using (var db = new ConquerorHubEntities())
            {
                var userId = db.SessionTokens.Where(a => a.SessionToken1 == sessionToken).SingleOrDefault().UserId;
                try
                {
                    var result = (from details in db.CH_ParticipantsGallery


                                  select new GalleryModel()
                                  {
                                      Id = details.Id,
                                      ImageData = details.ImageData,
                                      Caption = details.Caption,
                                      UserId = details.UserId,
                                      PostId = details.PostId,
                                      VideoData = details.VideoData,
                                      ContentType = details.ContentType,
                                      FileName = details.FileName,
                                      DateAndTime = details.DateAndTime


                                  }).ToList();
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        public async Task<List<ParticipationRegistrationModel>> GetParticipantRegistration(string sessionToken)
        {
            using (var db = new ConquerorHubEntities())
            {
                
                try
                {
                    var result = (from details in db.CH_ParticipantRegistration

                                
                                  select new ParticipationRegistrationModel()
                                  {
                                      Data =   details.Data ,
                                      
                                      OrganizerId = details.OrganizerId,
                                      ParticipantId = details.ParticipantId,
                                      EventId = details.EventId,
                                      VideoId = details.ParticipantsPostId,
                                      Name = details.Name,
                                      Qualification = details.Qualification,
                                      CollegeorSchool = details.CollegeorSchool,
                                      ContentType=details.ContentType,
                                      ParticipantStatus=details.ParticipantStatus,
                                      ContactNumber=details.ContactNumber,
                                      About_Self=details.About_Self,
                                      Modeofcompitetion=(modeofcompetation)details.Modeofcompitetion
                                      
                                     
                                      

                                  }).ToList();
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
      
        public async Task<List<EventRegistrationfromOrganizerModel>> GetEventRegistrationImageVideo(string sessionToken)
        {
            using (var db = new ConquerorHubEntities())
            {

                try
                {
                    var result = (from details in db.CH_EventRegistrationFromOrganizer


                                  select new EventRegistrationfromOrganizerModel()
                                  {

                                      Imagevideo = new EventsImageandVideo()
                                      {
                                          EventIdImageorVideo = details.EventId,
                                          OrganizerId = details.OrganizerId,
                                          EventImage = details.EventImage,
                                          EventVideo = details.EventVideo,
                                          imageorvideo = details.ImageorVideoforEvent,
                                          Homedisplayevent = details.Eventdisplayonhomepage,
                                          EventStatus=details.EventStatus,
                                          EventMode=details.modeofevent,
                                          Datetime=details.CurrentDateandTime
                                         
                                          
                                      }


                                  }).ToList();
                    return await Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        public async Task<Guid> SaveEventRegistrationImageVideo(string sessionToken, EventRegistrationfromOrganizerModel model)
        {
            using (var db = new ConquerorHubEntities())
            {

                try
                {
                     var data = db.CH_EventRegistrationFromOrganizer.Where(x => x.OrganizerId == model.Imagevideo.OrganizerId && x.EventId == model.Imagevideo.EventIdImageorVideo).Select(x => x).FirstOrDefault();
                    data.EventImage = model.Imagevideo.EventImage;
                    data.EventVideo = model.Imagevideo.EventVideo;
                    try
                    {
                        db.Entry(data).State = EntityState.Modified;

                        db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return await Task.FromResult(Guid.NewGuid());
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
        }
        public async Task<Guid> SaveParticipantGalleryVideo(string sessionToken, GalleryViewModel model)
        {
            var data1 = 0;
           using (var db = new ConquerorHubEntities())
            {

                var result = new CH_ParticipantsGallery()
                {

                    Id = model.GalleryData.Id,
                    ImageData = model.GalleryData.ImageData,
                    Caption = model.GalleryData.Caption,
                    UserId = model.GalleryData.UserId,
                    PostId = model.GalleryData.PostId,
                    VideoData = model.GalleryData.VideoData,
                    ContentType = model.GalleryData.ContentType,
                    DateAndTime = DateTime.UtcNow,
                    FileName = Path.GetFileName(model.GalleryData.UploadedPost.FileName)

                };


                try
                {
                    db.CH_ParticipantsGallery.Add(result);

                    data1 = db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return await Task.FromResult(Guid.NewGuid());
                
            }
        }
        public async Task<string> SavePerformanceFromparticipants(string sessionToken, ParticipationRegistrationModel model)
        {
            using (var db = new ConquerorHubEntities())
            {

                try
                {
                    var data1 = 0;
                    var ImageorVideo = db.CH_EventRegistrationFromOrganizer.Where(x => x.EventId == model.EventId).Select(x => x.ImageorVideoforEvent).FirstOrDefault();
                    var data = db.CH_ParticipantRegistration.Where(x => x.OrganizerId == model.OrganizerId && x.EventId == model.EventId).Count();
                    var model2 = db.CH_ParticipantRegistration.Where(x => x.OrganizerId == model.OrganizerId && x.EventId == model.EventId).FirstOrDefault();
                    if (ImageorVideo == 2)
                    {

                        byte[] bytes1;
                        using (BinaryReader br = new BinaryReader(model.PostedFile.InputStream))
                        {
                            bytes1 = br.ReadBytes(model.PostedFile.ContentLength);
                        }
                        if (data > 0)
                        {

                            model2.ContentType = Path.GetFileName(model.PostedFile.FileName);
                            model2.FileName = Path.GetFileName(model.PostedFile.FileName);
                            model2.Data = bytes1;
                            model2.ParticipantsPostId = Guid.NewGuid();
                            model2.ParticipantStatus = 2;


                        }
                        try
                        {
                            db.Entry(model2).State = EntityState.Modified;

                            data1 = db.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            throw ex;
                        }
                       

                        if (data1 == 1)
                        {
                            return await Task.FromResult("Your Video is uploaded you'll get email if you are shortlisted");
                            
                        }
                    }
                    else if (ImageorVideo == 1)
                    {
                        if (model.PostedFile != null)
                        {

                            MemoryStream target1 = new MemoryStream();
                            model.PostedFile.InputStream.CopyTo(target1);
                            byte[] Photo = target1.ToArray();
                            model.Data = Photo;
                            target1.Close();

                        }
                        if (data > 0)
                        {
                            model2.ContentType = Path.GetFileName(model.PostedFile.FileName);
                            model2.FileName = Path.GetFileName(model.PostedFile.FileName);
                            model2.Data = model.Data;
                            model2.ParticipantsPostId = Guid.NewGuid();
                            model2.ParticipantStatus = 2;

                        }
                        try
                        {
                            db.Entry(model2).State = EntityState.Modified;

                            data1 = db.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            throw ex;
                        }
                       

                        if (data1 == 1)
                        {
                            return await Task.FromResult("Your Image is uploaded you'll get email if you are shortlisted");
                          
                        }
                    }
                    try
                    {
                        db.Entry(model2).State = EntityState.Modified;

                        db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return await Task.FromResult("");


            }
        }

        public RequestResult<List<Sponsor1>> GetSponsorList(string sessionToken,Guid? UserId)
        {
            try
            {
                return _repository.GetSponsorList(sessionToken, UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<OrganizerAboutusModels> GetOrganizerABoutus(string sessionToken,Guid? userid)
        {
            try
            {
                return _repository.GetOrganizerAboutUs(sessionToken, userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}   
