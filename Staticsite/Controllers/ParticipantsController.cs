using Conquerorhub.Models;
using Conquerorhub.SDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using Staticsite.Controllers;
using System.IO;

using Staticsite.App_Start;
using Conquerorhub.SDK.EntityModel;
using System.Threading.Tasks;

namespace Staticsite.Controllers
{
    [SessionTimeOut]
    public class ParticipantsController : BaseController
    {
         private OrganiserBasicDetailsServices services;
        public ParticipantsController()
        {
            services = new OrganiserBasicDetailsServices();
        }
        // GET: Participants
        public ActionResult ParticipantAbout(Guid?Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            var services = new ParticipantServices();
          ParticipantAboutModel result = new ParticipantAboutModel();
            try
            {
                result = services.GetParticipantAboutdetails(SessionToken, Userid).Entity.FirstOrDefault();
                return View(result);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult About(Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            var services = new ParticipantServices();
            ParticipantAboutModel result = new ParticipantAboutModel();
            try
            {
                
                result = services.GetParticipantAboutdetails(SessionToken, Userid).Entity.FirstOrDefault();
                if (result == null)
                {
                    return View();
                }
                else
                { return View(result); }
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }

        [HttpPost]
        public ActionResult ParticipantAbout(ParticipantAboutModel model)
        {
            var services = new ParticipantServices();
            model.Userid = User.Identity.GetUserId();
            model.Id = Guid.NewGuid();
            try
            {
                var result = services.SaveParticipantbasicdetails(SessionToken, model).Entity;
                return RedirectToAction("ParticipantAbout");
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
            
        }
        //public ActionResult ProfilePage(Guid? UserId)
        //{
        //    if (UserId != null)
        //    {
        //        ViewBag.UserId = UserId;
        //    }
        //    return View();

        //}
        public async Task<ActionResult> ParticipantHome(Guid? UserId)
        {

            if (UserId != null)
            {
                ViewBag.UserId = UserId;
            }
            else
            {
                UserId = Guid.Parse(User.Identity.GetUserId());
            }


            var aspnetUser = new ApplicationMandatoryService();
            var basicfunctionality = new BasicFunctionalityofentireappService();
            //HomePage block
            var data = await services.GetHomePage(SessionToken);
            var result = new List<Home>();
            if (data != null)
            {
                result = data.Where(x => x.UserId == UserId.ToString()).Select(x => x).ToList();
            }
            var obj = new CH_HomeViewModel();
            obj.homeList = new List<Home>();
            var profilepic = basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == UserId.ToString()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == UserId.ToString());

            if (profilepic != null)
            {

            }
            if (result.Count != 0)
            {

                foreach (var item in result)
                {
                    obj.homeList.Add(new Home()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        PostText = item.PostText,
                        Image2 = item.Image == null ? null : Imageget(item.Image),
                        Video = item.Video,
                        DateTime = item.DateTime,
                        UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                        Profilepic = Imageget(profilepic != null ? profilepic.ProfilePicData : null),
                        Postid = item.Postid

                    });
                }
            }

            //Getting subscribed to userid
            obj.subscriberViewModel = new SubscribersViewmodel();
            obj.subscriberViewModel.Subcsriberslist = new List<SubscribersModel>();
            var basicdata = new BasicFunctionalityofentireappService();
            var subscribedToData = basicdata.GetTotalSubscribersperProfile(SessionToken).Entity;
            obj.subscriberViewModel.Subcsriberslist = subscribedToData.Where(x => x.SubscriberUserId == User.Identity.GetUserId() && x.SubscriptionStatus == true).Select(x => x).ToList();
            if (obj.subscriberViewModel.Subcsriberslist.Count() != 0)
            {
                var xyz = obj.subscriberViewModel.Subcsriberslist.Select(x => x.ProfileUserId);
                ViewBag.ProfileUserId = xyz.ToList();
            }

            else
            {

                ViewBag.ProfileUserId = null;
            }
            //Get like list  and comment list
            Sponsor1 sp = new Sponsor1();

            obj.sponsorSingleData = new Sponsor1();
            obj.sponsorSingleData.LikeList = new List<LikesModel>();
            obj.sponsorSingleData.LikeList.AddRange(basicfunctionality.GetTotalLikesOfPost(SessionToken).Entity.Where(y => y.SourceUserId == User.Identity.GetUserId()).Select(x => x).ToList());
            obj.sponsorSingleData.commentList = new List<CommentModel>();
            obj.sponsorSingleData.commentList.AddRange(basicfunctionality.GetcommentCount(SessionToken).Entity.Where(y => y.SourceUserId == User.Identity.GetUserId()).Select(x => x).ToList());



            if (ViewBag.ProfileUserId != null)
            {
                foreach (var item in ViewBag.ProfileUserId)
                {
                    obj.sponsorSingleData.LikeList.AddRange(basicfunctionality.GetTotalLikesOfPost(SessionToken).Entity.Where(y => y.SourceUserId == item).Select(x => x).ToList());
                }
            }
            if (ViewBag.ProfileUserId != null)
            {
                foreach (var item in ViewBag.ProfileUserId)
                {
                    obj.sponsorSingleData.commentList.AddRange(basicfunctionality.GetcommentCount(SessionToken).Entity.Where(y => y.SourceUserId == item).Select(x => x).ToList());
                }
            }
            //Event uploaded post
            var eventpost = await services.GetEventRegistrationImageVideo(SessionToken);
            obj.ImageVideolist = new List<EventsImageandVideo>();
            var getSubscribeuser = new List<EventRegistrationfromOrganizerModel>();
            if (ViewBag.ProfileUserId != null)
            {
                if (eventpost.Count != 0)
                {
                    foreach (var item in ViewBag.ProfileUserId)
                    {
                        getSubscribeuser.AddRange(eventpost.Where(x => x.Imagevideo.OrganizerId == item).Select(x => x).ToList());
                    }
                    foreach (var item in getSubscribeuser)
                    {
                        obj.ImageVideolist.Add(new EventsImageandVideo()
                        {
                            EventIdImageorVideo = item.Imagevideo.EventIdImageorVideo,
                            OrganizerId = item.Imagevideo.OrganizerId,
                            Image = item.Imagevideo.EventImage != null ? Imageget(item.Imagevideo.EventImage) : null,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.Imagevideo.OrganizerId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId).ProfilePicData),



                        });
                    }
                }

            }


            //Subscribed to home post (uploaded post)

            obj.Homelistofsubscribed = new List<Home>();
            var data4 = await services.GetHomePage(SessionToken);
            var getsubscriberhome = new List<Home>(); ;
            if (ViewBag.ProfileUserId != null)
            {
                if (data4.Count != 0)
                {
                    foreach (var item in ViewBag.ProfileUserId)
                    {
                        getsubscriberhome.AddRange(data4.Where(x => x.UserId == item).Select(x => x).ToList());
                    }
                    foreach (var item in getsubscriberhome)
                    {
                        obj.Homelistofsubscribed.Add(new Home()
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            PostText = item.PostText,
                            Image2 = item.Image == null ? null : Imageget(item.Image),
                            Video = item.Video,
                            DateTime = item.DateTime,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),
                            Postid = item.Postid

                        });
                    }
                }
            }
            //get sponsor uploaded data

            var sponsorlist = services.GetSponsorList(SessionToken, UserId);
            obj.sponsorListdata = new List<Sponsor1>();
            var list = new List<Sponsor1>();
            if (ViewBag.ProfileUserId != null)
            {
                foreach (var item in ViewBag.ProfileUserId)
                {

                    list.AddRange(sponsorlist.Entity.Where(x => x.UserId == item).Select(x => x).OrderBy(m => m.DateandTime).ToList());
                }
                foreach (var item in list)
                {
                    obj.sponsorListdata.Add(new Sponsor1()
                    {
                        Id = item.Id,
                        Image = item.Image,

                        Image1 = Imageget(item.Image),
                        ImageId = item.ImageId,
                        UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                        Caption = item.Caption,
                        DateandTime = item.DateandTime,
                        UserId = item.UserId,
                        profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData)
                    });
                }

            }
            //Participant gallery(uploaded post)
            obj.GalleryList = new List<GalleryModel>();
            var participantGalleryList = await services.GetParticipantGallerydata(SessionToken);
            var filteredParticipantGallery = new List<GalleryModel>();
            if (ViewBag.ProfileUserId != null)
            {
                if (participantGalleryList.Count != 0)
                {

                    foreach (var item in ViewBag.ProfileUserId)
                    {

                        filteredParticipantGallery = participantGalleryList.Where(x => x.UserId == item).ToList();


                    }
                }
                foreach (var item in filteredParticipantGallery)
                {
                    obj.GalleryList.Add(new GalleryModel()
                    {
                        Id = item.Id,

                        Caption = item.Caption,
                        UserId = item.UserId,
                        PostId = item.PostId,
                        VideoData = item.VideoData != null ? item.VideoData : null,
                        ContentType = item.ContentType,
                        Image = item.ImageData != null ? Imageget(item.ImageData) : null,
                        UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                        Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),
                        DateAndTime = item.DateAndTime


                    });
                }
            }
            //get shared post of subscribed to users   (shared post)     
            var basicFunctionality = new BasicFunctionalityofentireappService();
            var totalShare = basicFunctionality.GetTotalShare(SessionToken).Entity;
            var sharefilter = new List<Guid?>();
            var sourceCount = new List<ShareModel>();
            if (ViewBag.ProfileUserId != null)
            {

                foreach (var item in ViewBag.ProfileUserId)
                {
                    sharefilter = totalShare.Where(x => x.DestinationPage == item).Select(x => x.PostId).ToList();
                    sourceCount = totalShare.Where(x => x.SourcePage == item).Select(x => x).ToList();
                }
            }
            //Sponsored shared data
            obj.sponsorSingleData.sharelist = new List<ShareModel>();
            obj.sponsorSingleData.sharelist.AddRange(totalShare.ToList());
            obj.sponsorMultipleData = new List<Sponsor1>();
            var list1 = new List<Sponsor1>();
            if (sharefilter != null)
            {
                foreach (var item in sharefilter)
                {

                    list1.AddRange(sponsorlist.Entity.Where(x => x.ImageId == item).Select(x => x).OrderBy(m => m.DateandTime).ToList());
                }
                foreach (var item in list1)
                {
                    obj.sponsorMultipleData.Add(new Sponsor1()
                    {
                        Id = item.Id,
                        Image = item.Image,
                        Image1 = Imageget(item.Image),
                        ImageId = item.ImageId,
                        UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                        Caption = item.Caption,
                        DateandTime = item.DateandTime,
                        UserId = item.UserId,
                        profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),

                    });
                }

            }

            //participant shared post

            var participantRegistration = await services.GetParticipantRegistration(SessionToken);
            var participantregdata = new List<ParticipationRegistrationModel>();
            obj.Participantregistration = new List<ParticipationRegistrationModel>();
            if (sharefilter != null)
            {
                if (participantRegistration.Count != 0)
                {
                    foreach (var item in sharefilter)
                    {
                        participantregdata.AddRange(participantRegistration.Where(x => x.VideoId == item).Select(x => x).ToList());
                    }
                    foreach (var item in participantregdata)
                    {
                        obj.Participantregistration.Add(new ParticipationRegistrationModel()
                        {
                            Data = item.ContentType == "mp4" ? item.Data : null,
                            Images = item.ContentType != "mp4" ? Imageget(item.Data) : null,
                            OrganizerId = item.OrganizerId,
                            ParticipantId = item.ParticipantId,
                            EventId = item.EventId,
                            VideoId = item.VideoId,
                            Name = item.Name,
                            Qualification = item.Qualification,
                            CollegeorSchool = item.CollegeorSchool,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.OrganizerId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.OrganizerId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.OrganizerId).ProfilePicData),


                        });
                    }
                }
            }
            //participant Gallery shared

            obj.GalleryListSubscribed = new List<GalleryModel>();
            var participantGalleryListSubscribed = await services.GetParticipantGallerydata(SessionToken);
            var gallerylist = new List<GalleryModel>();
            if (sharefilter != null)
            {
                if (participantGalleryListSubscribed.Count != 0)
                {
                    foreach (var item in sharefilter)
                    {
                        gallerylist.AddRange(participantGalleryListSubscribed.Where(x => x.PostId == item).Select(x => x).ToList());
                    }
                    foreach (var item in gallerylist)
                    {
                        obj.GalleryListSubscribed.Add(new GalleryModel()
                        {
                            Id = item.Id,
                            Image = item.ImageData == null ? null : Imageget(item.ImageData),
                            Caption = item.Caption,
                            UserId = item.UserId,
                            PostId = item.PostId,
                            VideoData = item.VideoData,
                            ContentType = item.ContentType,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),
                            DateAndTime = item.DateAndTime
                        });

                    }
                }
            }




            return View(obj);

        }
        public async Task<ActionResult> ParticipantProfile(Guid? userid)
        { 
        if (userid != null)
            {
                ViewBag.UserId = userid;
                var usertype = UserManager.FindById(userid.ToString()).Usertype;
                if (usertype == 1)
                {
                    return RedirectToAction("Home", "OrganizerBasicDetails",new { UserId=userid });
                }
            }
        else if (userid == null)
            {
                userid =new Guid(User.Identity.GetUserId());
                var usertype = UserManager.FindById(User.Identity.GetUserId()).Usertype;
                if (usertype == 1)
                {
                    return RedirectToAction("Home", "OrganizerBasicDetails");
                }

            }

            var basicfunctionality = new BasicFunctionalityofentireappService();
            var aspnetUser = new ApplicationMandatoryService();
            HomePageViewModel obj = new HomePageViewModel();
            obj.aboutEventlist = new List<AboutEvent>();
            obj.aboutParticipantslist1 = new List<AboutParticipants>();
            obj.importantDateslist1 = new List<ImportantDates>();
            obj.awardRewardlist1 = new List<AwardsAndRewards>();
            EventsServices serviceObj = new EventsServices();
            var commentlist = basicfunctionality.GetcommentCount(SessionToken).Entity.Where(y => y.DestinationUserId == UserId.ToString()).Select(x => x).ToList();
            var rsult = basicfunctionality.GetTotalLikesOfPost(SessionToken).Entity.Where(y => y.DestinationUserId == UserId.ToString()).Select(x => x).ToList();
            Sponsor1 sp = new Sponsor1();

            obj.sponsorSingleData = new Sponsor1();
            obj.sponsorSingleData.LikeList = new List<LikesModel>();
            obj.sponsorSingleData.commentList = new List<CommentModel>();
            obj.sponsorSingleData.commentList.AddRange(commentlist);
            obj.sponsorSingleData.LikeList.AddRange(rsult);
            var services = new OrganiserBasicDetailsServices();
            var status1 = await services.GetEventRegistrationImageVideo(SessionToken);
            var status = status1.Where(x => x.Imagevideo.OrganizerId == UserId.ToString() && x.Imagevideo.EventStatus == 1).Select(x => x).ToList();
            obj.EventRegistration = new List<EventRegistrationfromOrganizerModel>();


            foreach (var item in status)
            {



                try
                {
                    // obj.aboutEventlist.Add(serviceObj.GetRegistrationAboutEvent(SessionToken, UserId).Entity.Where(x => x.EventId == item.Imagevideo.EventId && x.EventStatus==1).FirstOrDefault());
                    //obj.aboutParticipantslist1.Add(serviceObj.GetORegistrtionAboutParticipant(SessionToken, UserId).Entity.Where(x => x.EventId == item.Imagevideo.EventId && x.EventStatus == 1).FirstOrDefault());
                    // obj.awardRewardlist1.Add(serviceObj.GetAwardAndReward(SessionToken, UserId).Entity.Where(x => x.EventId == item.Imagevideo.EventId && x.EventStatus == 1).FirstOrDefault());
                    //obj.importantDateslist1.Add(serviceObj.GetImportantDateAndTime(SessionToken, UserId).Entity.Where(x => x.EventId == item.Imagevideo.EventId && x.EventStatus == 1).FirstOrDefault());

                    obj.EventRegistration.Add(new EventRegistrationfromOrganizerModel()
                    {

                        Imagevideo = new EventsImageandVideo()
                        {
                            EventIdImageorVideo = item.Imagevideo.EventIdImageorVideo,
                            OrganizerId = item.Imagevideo.OrganizerId,
                            Image = item.Imagevideo.EventImage != null ? Imageget(item.Imagevideo.EventImage) : null,
                            EventVideo = item.Imagevideo.EventVideo,
                            imageorvideo = item.Imagevideo.imageorvideo,
                            Homedisplayevent = item.Imagevideo.Homedisplayevent,
                            EventStatus = item.Imagevideo.EventStatus,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData)



                        }


                    });
                }
                catch (Exception ex)
                {
                    return View("~/Views/Errorpage/Errorpage.cshtml");
                }

            }



            obj.Sharedata = new List<ShareModel>();

            var PostId = new List<Guid?>();
            var data = new BasicFunctionalityofentireappService();

            var result = data.GetTotalShare(SessionToken).Entity;
            if (result != null)
            {

                obj.Sharedata = result.Where(x => x.DestinationPage == UserId.ToString()).Select(x => x).ToList() == null ? null : result.Where(x => x.DestinationPage == UserId.ToString()).Select(x => x).ToList();
                if (obj.Sharedata.Count() != 0)
                {
                    var xyz = obj.Sharedata.Select(x => x.PostId).ToList();
                    PostId = xyz;
                    ViewBag.Postid = PostId;
                }
                else
                {

                    ViewBag.Postid = null;
                }
            }


            //Get sponsored shred data
            var sponsorlist = services.GetSponsorList(SessionToken, UserId);
            obj.sponsorMultipleData = new List<Sponsor1>();
            obj.sponsorSingleData.sharelist = new List<ShareModel>();
            if (result != null)
            {
                obj.sponsorSingleData.sharelist.AddRange(result);
            }
            var sponsortemp = new List<Sponsor1>();
            if (ViewBag.Postid != null)
            {
                if (sponsorlist.Entity.Count != 0)
                {
                    foreach (var item in ViewBag.Postid)
                    {
                        sponsortemp.AddRange(sponsorlist.Entity.Where(a => a.ImageId == item).Select(a => a).ToList());
                    }
                    foreach (var item in sponsortemp)
                    {
                        obj.sponsorMultipleData.Add(new Sponsor1()
                        {
                            Id = item.Id,
                            Image = item.Image,
                            Image1 = Imageget(item.Image),
                            ImageId = item.ImageId,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString() == null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
                            Caption = item.Caption,
                            DateandTime = item.DateandTime,
                            UserId = User.Identity.GetUserId(),
                            profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData)

                        });
                    }
                }
            }
            //get participant gallery post shared
            obj.GalleryList = new List<GalleryModel>();
            var participantGalleryList = await services.GetParticipantGallerydata(SessionToken);
            var listtemp = new List<GalleryModel>();
            if (ViewBag.Postid != null)
            {
                if (participantGalleryList.Count != 0)
                {
                    foreach (var item in ViewBag.Postid)
                    {
                        listtemp.AddRange(participantGalleryList.Where(a => a.PostId == item && a.UserId == UserId.ToString()).Select(a => a).ToList());
                    }
                    foreach (var item in listtemp)
                    {
                        obj.GalleryList.Add(new GalleryModel()
                        {
                            Id = item.Id,
                            Image = item.ImageData == null ? null : Imageget(item.ImageData),
                            Caption = item.Caption,
                            UserId = item.UserId,
                            PostId = item.PostId,
                            VideoData = item.VideoData,
                            ContentType = item.ContentType,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (ViewBag.UserId as string == null ? User.Identity.GetUserId() : ViewBag.UserId as string)).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData)


                        });
                    }
                }
            }

            var participantRegistration = await services.GetParticipantRegistration(SessionToken);
            var participantregdata = new List<ParticipationRegistrationModel>();
            obj.Participantregistration = new List<ParticipationRegistrationModel>();
            string extension = "";

            if (ViewBag.Postid != null)
            {
                if (participantRegistration.Count != 0)
                {
                    foreach (var item in ViewBag.Postid)
                    {
                        participantregdata.AddRange(participantRegistration.Where(x => x.VideoId == item).Select(x => x).ToList());
                    }

                    foreach (var item in participantregdata)
                    {
                        extension = item.ContentType.Split('.').Last();
                        obj.Participantregistration.Add(new ParticipationRegistrationModel()
                        {
                            Data = extension == "mp4" ? item.Data : null,
                            Images = extension != "mp4" ? Imageget(item.Data) : null,
                            OrganizerId = item.OrganizerId,
                            ParticipantId = item.ParticipantId,
                            EventId = item.EventId,
                            VideoId = item.VideoId,
                            Name = item.Name,
                            Qualification = item.Qualification,
                            CollegeorSchool = item.CollegeorSchool,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString() == null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData)
                        });
                    }
                }
            }
            obj.homeList = new List<Home>();
            var data4 = await services.GetHomePage(SessionToken);
            var homeData = new List<Home>();
            if (ViewBag.Postid != null)
            {
                if (data4.Count != 0)
                {
                    foreach (var item in ViewBag.Postid)
                    {
                        homeData.AddRange(data4.Where(x => x.Postid == item).Select(x => x).ToList());
                    }
                    foreach (var item in homeData)
                    {
                        obj.homeList.Add(new Home()
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            PostText = item.PostText,
                            Image2 = item.Image == null ? null : Imageget(item.Image),
                            Video = item.Video,
                            DateTime = item.DateTime,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString() == null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData),
                            Postid = item.Postid
                        });
                    }
                }
            }

            obj.Imagevideolist = new List<EventsImageandVideo>();
            var data5 = await services.GetEventRegistrationImageVideo(SessionToken);
            var eventData = new List<EventRegistrationfromOrganizerModel>();
            if (ViewBag.Postid != null)
            {
                if (data5.Count != 0)
                {
                    foreach (var item in ViewBag.Postid)
                    {
                        eventData.AddRange(data5.Where(x => x.Imagevideo.EventIdImageorVideo == item).Select(x => x).ToList());
                    }
                    foreach (var item in eventData)
                    {
                        obj.Imagevideolist.Add(new EventsImageandVideo()
                        {
                            EventIdImageorVideo = item.Imagevideo.EventIdImageorVideo,
                            OrganizerId = item.Imagevideo.OrganizerId,
                            Image = item.Imagevideo.EventImage == null ? null : Imageget(item.Imagevideo.EventImage),
                            EventVideo = item.Imagevideo.EventVideo,



                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString() == null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData),

                        });
                    }
                }
            }


            return View(obj);

        }

        public string Imageget(byte[] Image)
        {
            if (Image != null)
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
            return null;
        }
        public ActionResult ParticipantPhotoGallery(Guid? userid)
        {
            
            var services = new ParticipantServices();
            GalleryViewModel obj = new GalleryViewModel();
            obj.GalleryList =new List<GalleryModel>();
            try
            {
             
                if (userid == null)
                {
                    obj.GalleryList = services.GetParticipantPhotoGallery(SessionToken, userid).Entity.Where(x => x.UserId == User.Identity.GetUserId()).OrderBy(m => m.DateAndTime).ToList();
                }
                if (userid != null)
                {
                    ViewBag.UserId = userid;
                    obj.GalleryList = services.GetParticipantPhotoGallery(SessionToken, userid).Entity.Where(x => x.UserId == userid.ToString()).OrderBy(m => m.DateAndTime).ToList();
                }
                ViewBag.truestatus = Session["truestatus"];
                ViewBag.status = Session["status"];
                var data = new BasicFunctionalityofentireappService();

                var rsult = data.GetTotalLikesOfPost(SessionToken).Entity;
                var comment = data.GetcommentCount(SessionToken).Entity;
                obj.commentData = new List<CommentModel>();
                obj.commentData.AddRange(comment);



             obj.likesdata = new List<LikesModel>();

                obj.likesdata.AddRange(rsult);
                
                foreach (var item in obj.GalleryList)
                {
                    if (item.ImageData != null)
                    {

                        Stream inputstream = new MemoryStream(item.ImageData, 0, item.ImageData.Length);
                        MemoryStream memoryStream = inputstream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputstream.CopyTo(memoryStream);
                        }

                        string imreBase64Data = Convert.ToBase64String(item.ImageData);
                        string imgDataURL2 = string.Format("data:image2/png;base64,{0}", imreBase64Data);
                        item.Images = new List<string>();
                        item.Images.Add(imgDataURL2);
                    }

                }

                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
           
        }
        [HttpPost]
        public ActionResult ParticipantPhotoGallery(GalleryViewModel model)
        {

            model.GalleryData.UserId = User.Identity.GetUserId();
            model.GalleryData.PostId = Guid.NewGuid();
            
            var services = new ParticipantServices();
            try
            {
                var returnData = services.SavePhotoGallery(SessionToken, model);
                return RedirectToAction("ParticipantPhotoGallery", "Participants");
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public async Task<ActionResult> ParticipantVideoGallery(Guid? Userid)
        {
            var obj = new GalleryViewModel();
            var model = new List<GalleryModel>();
            var data = new List<GalleryModel>();
            try
            {
                var user = "";
                if (Userid == null)
                {
                     user = User.Identity.GetUserId();
                }
                if (Userid != null)
                {
                    ViewBag.UserId = Userid;
                    user = Userid.ToString();

                }

                OrganiserBasicDetailsServices obj1 = new OrganiserBasicDetailsServices();



                if (user!= null)
                {
                   var model1 = await obj1.GetParticipantGallerydata(SessionToken);
                    model = model1.Where(x => x.UserId == user).Select(x => x).ToList();
                   

                    foreach (var item in model)
                    {
                        if (item.VideoData != null) {
                            data.Add(new GalleryModel()
                            {
                                Id = item.Id,
                                ImageData = item.ImageData,
                                Caption = item.Caption,
                                UserId = item.UserId,
                                PostId = item.PostId,
                                VideoData = item.VideoData,
                                ContentType = item.ContentType


                            });
                        }
                        
                    }
                }
                obj.GalleryList = new List<GalleryModel>();
                obj.GalleryList.AddRange(data);
                obj.GalleryData = new GalleryModel();
                obj.likesdata = new List<LikesModel>();
                var data2 = new BasicFunctionalityofentireappService();
                var rsult = data2.GetTotalLikesOfPost(SessionToken).Entity;
                var comment = data2.GetcommentCount(SessionToken).Entity;
                obj.commentData = new List<CommentModel>();
                obj.commentData.AddRange(comment);

                obj.likesdata.AddRange(rsult);
                return View(obj);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult ParticipantVideoGallery(GalleryViewModel model)
        {
            
            byte[] bytes1;
            using (BinaryReader br = new BinaryReader(model.GalleryData.UploadedPost.InputStream))
            {
                bytes1 = br.ReadBytes(model.GalleryData.UploadedPost.ContentLength);
            }
            try {
                model.GalleryData.PostId = Guid.NewGuid();
                model.GalleryData.UserId = User.Identity.GetUserId();
                model.GalleryData.Id = Guid.NewGuid();
                var filename = Path.GetFileName(model.GalleryData.UploadedPost.FileName);
                string extension = filename.Split('.').Last();
                model.GalleryData.VideoData = bytes1;
                model.GalleryData.ContentType = extension;

                OrganiserBasicDetailsServices organizerBasicdetail = new OrganiserBasicDetailsServices();
               var result= organizerBasicdetail.SaveParticipantGalleryVideo(SessionToken, model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
            
            return RedirectToAction("ParticipantVideoGallery", "Participants");
        }
        public ActionResult ParticipantRegistration(Guid ?EventId,Guid?Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;

            }
            EventsServices serviceObj = new EventsServices();
            ViewBag.OnlineorOffline = serviceObj.GetRegistrationAboutEvent(SessionToken,Userid).Entity.Where(x=>x.EventId==EventId).Select(x=>x.modeofevent).FirstOrDefault();
            ParticipationRegistrationModel obj = new ParticipationRegistrationModel();
            obj.EventId = EventId;
            obj.OrganizerId = Userid.ToString();
            obj.ParticipantId = User.Identity.GetUserId();

            var returneduserid = TempData["ReturnedValue"];
            var userid = User.Identity.GetUserId();
            try
            {
                if (returneduserid != null)
                {
                    if (returneduserid.ToString() != userid)
                    {
                        EventsController controllerobj = new EventsController();
                        var result = controllerobj.Otpcheck(EventId, 0);
                        ViewBag.Success = "You have successfully registered.Otp is sent to your email.Please use that otp to upload your performance";

                    }
                    if (returneduserid.ToString() == userid)
                    {
                        TempData["Caution"] = "User already registered";
                        ViewBag.Success = null;
                    }
                   
                }


                return View(obj);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
    
        [HttpPost]
        public ActionResult ParticipantRegistration(ParticipationRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            model.ParticipantId = User.Identity.GetUserId();

        
            ParticipantServices obj = new ParticipantServices();
            try
            {
                var result = obj.SaveParticipantsregistration(SessionToken, model).Entity.Result;
                TempData["ReturnedValue"] = result;
                return RedirectToAction("ParticipantRegistration", new { EventId = model.EventId, Userid = model.OrganizerId });
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult Gallery()
        {
            return View();
        }
        [HttpGet]
        public async Task<FileResult> DownloadFile(Guid? VideoID)
        {

            var result = new ParticipationRegistrationModel();
            try
            {
                OrganiserBasicDetailsServices obj1 = new OrganiserBasicDetailsServices();
                var result1 =await obj1.GetParticipantRegistration(SessionToken);

                result=result1.ToList().Find(x => x.VideoId == VideoID);
               
                return File(result.Data, result.ContentType, result.FileName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<FileResult> DownloadFile1(Guid? VideoID)
        {

            var result = new GalleryModel();
                try
            {
                OrganiserBasicDetailsServices obj1 = new OrganiserBasicDetailsServices();
                var result1 = await obj1.GetParticipantGallerydata(SessionToken);

                result = result1.ToList().Find(x => x.PostId == VideoID);

                return File(result.VideoData, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}