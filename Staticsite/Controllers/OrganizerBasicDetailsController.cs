using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Conquerorhub.Models;
using System.IO;
using Conquerorhub.SDK.Services;
using Microsoft.AspNet.Identity;
using Staticsite.App_Start;

using System.Threading.Tasks;
using Conquerorhub.SDK.EntityModel;

namespace Staticsite.Controllers
{
    [SessionTimeOut]
    public class OrganizerBasicDetailsController : BaseController
    {
        private OrganiserBasicDetailsServices services;
        public OrganizerBasicDetailsController()
        {
            services = new OrganiserBasicDetailsServices();
        }
        // GET: OrganizerBasicDetails
        public ActionResult AboutUs(Guid? userid)
        {
            if (userid != null)
            {
                ViewBag.UserId = userid;
            }
           
            OrganizerAboutusModels obj = new OrganizerAboutusModels();
            try
            {
                obj = services.GetOrganizerABoutus(SessionToken, userid).Entity;
                ViewBag.data = obj;
                if (obj == null)
                {
                    return View();
                }
                else return View(obj);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult AboutUs(OrganizerAboutusModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
         
            model.UserId = User.Identity.GetUserId();
            try
            {
                var returnData = services.SaveOrganizerAboutUs(SessionToken, model);

                return RedirectToAction("AboutUs");
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        public ActionResult About(Guid? UserId)
        {
            if (UserId != null)
            {
                ViewBag.UserId = UserId;
            }
          else { UserId = Guid.Parse(User.Identity.GetUserId()); }
            OrganizerAboutusModels obj = new OrganizerAboutusModels();
            try
            {
                obj = services.GetOrganizerABoutus(SessionToken, UserId).Entity;
                ViewBag.data = obj;
                if (obj== null)
                {
                    return View();
                }
                else return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        
        public ActionResult CheckForActiveAccount()
        {
            var userid = User.Identity.GetUserId();
            var activation = UserManager.FindById(userid);
            if (activation.UserActive == false)
            {
                return View("~/Views/Account/DeactivatedPage.cshtml");
            }
            else return null;

        }
     
        public ActionResult Home1(string name)
        {
            string Email = null;
            Guid UserId = Guid.Empty;
            //  var e = Request.Form["MyInput"].ToString();
            Email = name;
            bool? accountActivation = null;
            // var Email = fc["MyInput"].ToString();
            var obj = new ApplicationMandatoryService();    var userdata = Session["UserData"] as List<AspnetUsersModel>;
            if (userdata != null)
            {
                foreach (var i in userdata)
                {
                    if (Email == i.Email)
                  { UserId = Guid.Parse(i.Id);
                    accountActivation = i.UserActive;

                        break; }
                }
                if (accountActivation == false)
                {
                    return View("~/Views/Account/UserDeactivatedView.cshtml");
                }
            }

            var basicService = new BasicFunctionalityofentireappService();
            var blockModel = new BlockModel();
     
           
                blockModel.BlockedUser = User.Identity.GetUserId();
                blockModel.BlockingUser = UserId.ToString();
           
            var blockedCheck = basicService.GetBlockUser(SessionToken,blockModel).Entity.Status;
            if (blockedCheck != null)
            {
                if (blockedCheck == true)
                {
                    return View("~/Views/Account/UserDeactivatedView.cshtml");
                }
            }

            return RedirectToAction("ProfilePage", new { UserId = UserId });
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
        public async Task<ActionResult> Home(Guid? UserId)
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
            var result= new List<Home>();
            if (data != null)
            {
                result = data.Where(x => x.UserId == UserId.ToString()).Select(x => x).ToList();
            }
            var obj = new CH_HomeViewModel();
            obj.homeList = new List<Home>();
            var profilepic = basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == UserId.ToString())==null?null: basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == UserId.ToString());
            
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
                        Postid=item.Postid
                       
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
            var getSubscribeuser= new List<EventRegistrationfromOrganizerModel>();
            if (ViewBag.ProfileUserId != null)
            {
                if (eventpost.Count != 0)
                {
                    foreach (var item in ViewBag.ProfileUserId)
                    {
                        getSubscribeuser.AddRange(eventpost.Where(x => x.Imagevideo.OrganizerId == item).Select(x => x).ToList());
                      }
                    foreach(var item in getSubscribeuser)
                    {
                        obj.ImageVideolist.Add(new EventsImageandVideo() { EventIdImageorVideo=item.Imagevideo.EventIdImageorVideo,
                            OrganizerId=item.Imagevideo.OrganizerId,
                            Image=item.Imagevideo.EventImage!=null?Imageget(item.Imagevideo.EventImage):null,
                            UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.Imagevideo.OrganizerId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId)==null?null: basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId).ProfilePicData),
                            


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
                            Postid=item.Postid

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
                       
                        Image1=Imageget(item.Image),
                        ImageId = item.ImageId,
                        UserName= aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
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
                        UserName= aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                        Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),
                        DateAndTime=item.DateAndTime


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
                          Image1=Imageget(item.Image),
                            ImageId = item.ImageId,
                            UserName= aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
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
            obj.Participantregistration= new List<ParticipationRegistrationModel>();
            if (sharefilter != null)
            {
                if (participantRegistration.Count != 0)
                {
                    foreach (var item in sharefilter)
                    {
                        participantregdata.AddRange( participantRegistration.Where(x => x.VideoId == item).Select(x => x).ToList());
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
                            UserName= aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.OrganizerId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.OrganizerId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.OrganizerId).ProfilePicData),
                            

                        });
                    }
                }
            }
            //participant Gallery shared

            obj.GalleryListSubscribed = new List<GalleryModel>();
            var participantGalleryListSubscribed = await services.GetParticipantGallerydata(SessionToken);
            var gallerylist= new List<GalleryModel>();
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
                           Image=item.ImageData==null?null:Imageget(item.ImageData),
                            Caption = item.Caption,
                            UserId = item.UserId,
                            PostId = item.PostId,
                            VideoData = item.VideoData,
                            ContentType = item.ContentType,
                            UserName= aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.UserId).UserName,
                            Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.UserId).ProfilePicData),
                            DateAndTime=item.DateAndTime
                        });

                    }
                }
            }

          
            

            return View(obj);

        }
        [HttpPost]
        public async Task<ActionResult> Home(CH_HomeViewModel model)
        {
            model.homeSingledata.Id = Guid.NewGuid();
            model.homeSingledata.Postid = Guid.NewGuid();
            model.homeSingledata.UserId = User.Identity.GetUserId();
            try
            {
                var result = await services.PostHome(model);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

            return RedirectToAction("Home");

        }
        public async Task<ActionResult> ProfilePage(Guid? UserId)
        {

            try
            {
                if (UserId != null)
                {

                    ViewBag.UserType = "Organizer";
                    var usertye = UserManager.FindById(UserId.ToString()).Usertype;
                    if (usertye == 2)
                    {
                        ViewBag.UserName = UserManager.FindById(UserId.ToString()).UserName;


                        ViewBag.UserId = UserId;
                        return RedirectToAction("ParticipantProfile", "Participants", new { userid = UserId });
                    }
                    ViewBag.UserId = UserId;

                }
                else if (UserId == null)
                {
                    UserId = new Guid(User.Identity.GetUserId());
                    var usertype = UserManager.FindById(User.Identity.GetUserId()).Usertype;
                    UserId = new Guid(User.Identity.GetUserId());
                    if (usertype == 2)
                    {
                        return RedirectToAction("ParticipantProfile", "Participants");
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
                var status = status1.Where(x => x.Imagevideo.OrganizerId == UserId.ToString() && x.Imagevideo.EventStatus==1).Select(x => x).ToList();
                obj.EventRegistration = new List<EventRegistrationfromOrganizerModel>();


                foreach (var item in status) {



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
                                UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString() == null? User.Identity.GetUserId(): UserId.ToString())).UserName,
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
                var listtemp= new List<GalleryModel>();
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
                                UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString()==null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
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
                                Postid=item.Postid
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
                                Image = item.Imagevideo.EventImage==null?null: Imageget(item.Imagevideo.EventImage),
                                EventVideo = item.Imagevideo.EventVideo,
                               
                                

                                UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == (UserId.ToString()== null ? User.Identity.GetUserId() : UserId.ToString())).UserName,
                                Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == User.Identity.GetUserId()).ProfilePicData),
                               
                            });
                        }
                    }
                }


                return View(obj);
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
            
          
        }


        public ActionResult Sponsor(Guid? userid)
        {
           
            Sponsors obj = new Sponsors();
            try
            {
                if (userid != null)
                {
                    ViewBag.UserId = userid;
                    obj.sponsorMultipleData = services.GetSponsorList(SessionToken, userid).Entity.Where(x => x.UserId == userid.ToString()).OrderBy(m => m.DateandTime).ToList();
                }

                if (userid == null)
                {
                    obj.sponsorMultipleData = services.GetSponsorList(SessionToken, userid).Entity.Where(x => x.UserId == User.Identity.GetUserId()).OrderBy(m => m.DateandTime).ToList();
                }

                ViewBag.truestatus = Session["truestatus"];
                ViewBag.status = Session["status"];
                var data = new BasicFunctionalityofentireappService();
                var rsult = data.GetTotalLikesOfPost(SessionToken).Entity;
                var sharecount = data.GetTotalShare(SessionToken).Entity;
                var comment = data.GetcommentCount(SessionToken).Entity;
              

                Sponsor1 sp = new Sponsor1();
                sp.LikeList = rsult;
                obj.sponsorSingleData = new Sponsor1();
                obj.sponsorSingleData.LikeList = new List<LikesModel>();
                obj.sponsorSingleData.commentList = new List<CommentModel>();
                obj.sponsorSingleData.commentList.AddRange(comment);
                obj.sponsorSingleData.LikeList.AddRange(sp.LikeList);
                obj.sponsorSingleData.sharelist = new List<ShareModel>();
                obj.sponsorSingleData.sharelist.AddRange(sharecount.ToList());
                foreach (var item in obj.sponsorMultipleData)
                {

                    Stream inputstream = new MemoryStream(item.Image, 0, item.Image.Length);
                    MemoryStream memoryStream = inputstream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputstream.CopyTo(memoryStream);
                    }

                    string imreBase64Data = Convert.ToBase64String(item.Image);
                    string imgDataURL2 = string.Format("data:image2/png;base64,{0}", imreBase64Data);
                    item.Images = new List<string>();
                    item.Images.Add(imgDataURL2);

                }
                if (obj != null)
                {
                    return View(obj);
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult Sponsor(Sponsors model)
        {
            model.sponsorSingleData.UserId = User.Identity.GetUserId();
            model.sponsorSingleData.ImageId = Guid.NewGuid();
            try
            {
              
                var returnData = services.SaveUserDeatils(SessionToken, model);


                return RedirectToAction("Sponsor", "OrganizerBasicDetails");
            }
            catch(Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
       
        public ActionResult Subscribers(Guid? UserId)
        {
            var obj1 = new ApplicationMandatoryService();
            var result1 = obj1.Getuserlist(SessionToken);
            SubscribersViewmodel obj = new SubscribersViewmodel();
           var data = new BasicFunctionalityofentireappService();
            var aspnetUser = new ApplicationMandatoryService();
            obj.Subcsriberslist = new List<SubscribersModel>();
            if (UserId != null)
            {
                ViewBag.UserId = UserId;
                var result = data.GetTotalSubscribersperProfile(SessionToken).Entity.Where(x => x.ProfileUserId == UserId.ToString() && x.SubscriptionStatus==true).Select(x => x).ToList();
                var result2 = data.GetTotalSubscribersperProfile(SessionToken).Entity.Where(x => x.SubscriberUserId == UserId.ToString() && x.SubscriptionStatus==true).Select(x => x).ToList();
                
                if (result != null)
                {
                    foreach (var item in result)
                    {
                  var user=  result1.Entity.FirstOrDefault(f => f.Id == item.SubscriberUserId);
                        obj.Subcsriberslist.Add(new SubscribersModel()
                        {

                            SubscriberUserId = user.Id,
                            Subscribersname = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == user.Id).UserName,
                            usertype = user.Usertype.ToString(),
                            SubscriptionStatus = item.SubscriptionStatus,
                          
                          profilepic = Imageget(data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId ==user.Id) == null ? null : data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId ==user.Id).ProfilePicData)
                        });       
                       


                    }
                }
                if (result2 != null)
                {
                    foreach (var item in result2)
                    {
                        var user = result1.Entity.FirstOrDefault(f => f.Id == item.ProfileUserId);
                        obj.Subcsriberslist.Add(new SubscribersModel()
                        {

                            ProfileUserId = user.Id,
                            Subscribedtoname = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == user.Id).UserName,
                            usertype = user.Usertype.ToString(),
                            SubscriptionStatus = item.SubscriptionStatus,
                            profilepicSubscribedto = Imageget(data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == user.Id) == null ? null : data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == user.Id).ProfilePicData)
                        }); ;
                       
                    }
                }
            
            }
            else
            {


                var result = data.GetTotalSubscribersperProfile(SessionToken).Entity.Where(x => x.ProfileUserId == User.Identity.GetUserId() && x.SubscriptionStatus==true).Select(x => x).ToList();
                var result2 = data.GetTotalSubscribersperProfile(SessionToken).Entity.Where(x => x.SubscriberUserId ==User.Identity.GetUserId() && x.SubscriptionStatus==true).Select(x => x).ToList();
                if (result != null)
                {

                    foreach (var item in result)
                    {
                        var user = result1.Entity.FirstOrDefault(f => f.Id == item.SubscriberUserId);
                        obj.Subcsriberslist.Add(new SubscribersModel()
                        {

                            SubscriberUserId = user.Id,
                            Subscribersname = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == user.Id).UserName,
                            usertype = user.Usertype.ToString(),
                            SubscriptionStatus = item.SubscriptionStatus,
                        });




                    }
                }
                if (result2 != null)
                {

                    foreach (var item in result2)
                    {
                        var user = result1.Entity.FirstOrDefault(f => f.Id == item.ProfileUserId);
                        obj.Subcsriberslist.Add(new SubscribersModel()
                        {

                            ProfileUserId = user.Id,
                            Subscribedtoname = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == user.Id).UserName,
                            usertype = user.Usertype.ToString(),
                            SubscriptionStatus = item.SubscriptionStatus,
                        }); ;
                     
                    }
                }
            }


            return View(obj);

           
                }
    }
}