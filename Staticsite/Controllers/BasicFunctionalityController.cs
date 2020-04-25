
using Conquerorhub.Models;
using Conquerorhub.SDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.IO;
using System.Drawing;
using System.Web.UI;
using Conquerorhub.SDK.EntityModel;
using System.Threading.Tasks;

namespace Staticsite.Controllers
{
    public class BasicFunctionalityController : BaseController
    {
        private BasicFunctionalityofentireappService data;
        public BasicFunctionalityController()
        {
            data = new BasicFunctionalityofentireappService();
        }
        public ActionResult GetLLikesList(string Id, string userid)

        {
            if (userid == null)
            {
                userid = User.Identity.GetUserId();
            }
            else
            {
                ViewBag.UserId = userid;
            }
            try
            {
                var result = data.GetTotalLikesOfPost(SessionToken);
                var truestatus = result.Entity.Where(a => a.LikeStatus == true && a.DestinationUserId == userid && a.Imageid == Guid.Parse(Id)).Select(a => a.LikeStatus).Count();
                var falsestatus = result.Entity.Where(a => a.LikeStatus == false && a.DestinationUserId == userid && a.Imageid == Guid.Parse(Id)).Select(a => a.LikeStatus).Count();
                var value = result.Entity.Where(x => x.DestinationUserId == User.Identity.GetUserId()).Select(x => x.LikeStatus);
                ViewBag.LikeTrueCount = truestatus;
                ViewBag.LikeFalseCount = falsestatus;
                return Json(new { truestatus, value }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult GetCommentCount(string Id,string userid)
        {
            try
            {
                var result = data.GetcommentCount(SessionToken);
                var count = result.Entity.Where(a => a.DestinationUserId == userid && a.PostId == Guid.Parse(Id)).Select(x => x).Count();
                return Json(new { count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }


        public List<AspnetUsersModel> Getuserlist(string Id)

        {
            try
            {
                var obj = new ApplicationMandatoryService();
                var result = obj.Getuserlist(SessionToken);
                var truestatus = result.Entity.ToList();

                return truestatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetSubscribersList(string Id)

        {

            if (Id != "")
            {
                ViewBag.UserId = Id;
            }
            else
            {
                Id = System.Web.HttpContext.Current.User.Identity.GetUserId(); ;
            }
            try
            {

                var result = data.GetTotalSubscribersperProfile(SessionToken);
                var truestatus = result.Entity.Where(a => a.SubscriptionStatus == true && a.ProfileUserId == Id).Select(a => a.SubscriptionStatus).Count();
                var falsestatus = result.Entity.Where(a => a.SubscriptionStatus == false && a.ProfileUserId == Id).Select(a => a.SubscriptionStatus).Count();
                var value = result.Entity.Where(x => x.ProfileUserId == Id).Select(x => x).ToList();

                if (value != null)

                { System.Web.HttpContext.Current.Session["SubcriberList"] = value; }
                else
                {
                    System.Web.HttpContext.Current.Session["SubcriberList"] = 0;

                }

                ViewBag.SubscribersCount = truestatus;
                ViewBag.SubscribersCountfalse = falsestatus;

                return ViewBag.SubscribersCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return Json(new { truestatus, value }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVotersList(string Id)

        {
          
            var result = data.GetTotalVoteperPost(SessionToken);
           int result1= result.Entity.Where(a => a.VoteStatus == true && a.PostId==new Guid(Id)).Select(a => a.VoteStatus).Count();
          
            var value = result.Entity.Where(x => x.userid == User.Identity.GetUserId()).Select(x => x.VoteStatus);


            return Json(new { result1, value }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShareList(Guid? Id)

        {
            try
            {
                var result = data.GetTotalShare(SessionToken);
                var ShareCount = result.Entity.Where(a => a.PostId == Id && a.DestinationPage == User.Identity.GetUserId()).Select(a => a.PostId).Count();
                //var value = result.Entity.Where(x => x.userid == User.Identity.GetUserId()).Select(x => x.VoteStatus);


                return Json(new { ShareCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult GetBlockList()

        {
            try
            {
                var aspnetUser = new ApplicationMandatoryService();
                var result1 = aspnetUser.Getuserlist(SessionToken);

                BlockViewModel blockModel = new BlockViewModel();
                blockModel.ProfilePic = new List<string>();
                blockModel.BlockedUserName = new List<string>();
                blockModel.usertype = new List<string>();
                blockModel.blockModelList = new List<BlockModel>();
                var result = data.GetBlockList(SessionToken).Entity.ToList();
                if (result.Count != 0)
                {
                    foreach (var item in result)
                    {
                        blockModel.BlockedUserName.Add(aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.BlockedUser).UserName);
                        blockModel.ProfilePic.Add(Imageget(data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.BlockedUser) == null ? null : data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.BlockedUser).ProfilePicData));
                        blockModel.usertype.Add(result1.Entity.FirstOrDefault(f => f.Id == item.BlockedUser).Usertype.ToString());
                    }

                    blockModel.blockModelList.AddRange(result);
                }
                else
                {
                    return View();
                }



                return View(blockModel);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }







        public PartialViewResult GetComments(string postId)
        {
            List<CommentModel> result = new List<CommentModel>();
            result = data.GetTotalComment(SessionToken, postId).Entity.ToList();

            return PartialView("~/Views/Shared/_MyComment.cshtml", result);

        }
        public PartialViewResult GetSubComments(string ComID)
        {
            List<SubCommentModel> result = new List<SubCommentModel>();
            result = data.GetSubComment(SessionToken, ComID).Entity.ToList();

            return PartialView("~/Views/Shared/_MySubComment.cshtml", result);

        }

        public ActionResult AddComment(string commentMsg, string postorcmtId ,string destinationId)
        {
            CommentModel data1 = new CommentModel();
            data1.PostId = Guid.Parse(postorcmtId);
            data1.CommentMessage = commentMsg;
            data1.SourceUserId = User.Identity.GetUserId();
            data1.DestinationUserId = destinationId;

            data1.Id = Guid.NewGuid();
            try
            {
                var Result = data.Savecomment(SessionToken, data1).Entity;

                if (Result != null)
                {
                    return Json(new
                    {
                        _result = Result.result,
                        CDate = Result.CommentDate,
                        CId = Result.CommentId,
                        cmt = Result.CommentMsg,
                        Name = Result.UserName
                    });
                }
                else
                    return Json(new { result = false });
            }
            catch (Exception ex)
            {
                return Json(new { result = false });
                // return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        public ActionResult AddSubComment(string CommentMsg, string postorcmtId ,string destinationId)
        {
            SubCommentModel data1 = new SubCommentModel();
            data1.CommentId = Guid.Parse(postorcmtId);
            data1.SubCommentmsg = CommentMsg;
            data1.SourceUserId = User.Identity.GetUserId();
            data1.SubCommentId = Guid.NewGuid();
            data1.DestinationUserId = destinationId;
            try
            {
                var Result = data.SaveSubcomment(SessionToken, data1).Entity;

                if (Result != null)
                {
                    return Json(new
                    {
                        _result = Result.result,
                        CDate = Result.CommentDate,
                        CId = Result.CommentId,
                        cmt = Result.CommentMsg,
                        Name = Result.UserName
                    });
                }
                else
                    return Json(new { _result = false });
            }
            catch (Exception ex)
            {
                return Json(new { result = false });
                // return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }

        [HttpPost]
        public ActionResult SaveLikes(bool status, Guid? id, string userid)
        {

            LikesModel model = new LikesModel();
            model.LikeStatus = status;
            model.Imageid = id;
            if (userid != null && userid != "")
            {
                model.DestinationUserId = userid;
            }
            else
            {
                model.DestinationUserId = User.Identity.GetUserId();
            }
            model.SourceUserId = User.Identity.GetUserId();
            try
            {
                var value = data.SaveLikes(SessionToken, model).Entity.Result;

                return Json(value, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }


        // [HttpPost]
        public ActionResult SaveSubscribers(bool status, Guid? UserId)
        {
            SubscribersModel model = new SubscribersModel();
            if (UserId == null)
            {
                model.ProfileUserId = User.Identity.GetUserId();
            }
            else
            {
                model.ProfileUserId = UserId.ToString();
            }
            model.SubscriptionStatus = status;



            model.SubscriberUserId = User.Identity.GetUserId();
            try
            {
                var value = data.SaveSubscribers(SessionToken, model);

                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult SaveVoters(bool status, Guid? id, Guid? Eventid)
        {

            VotesModel model = new VotesModel();
            model.VoteStatus = status;
            model.PostId = id;
            model.EventId = Eventid;
            model.userid = User.Identity.GetUserId();
            try
            {
                var value = data.SaveVoters(SessionToken, model);

                return Json(value, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult SaveShare(Guid? id, string sourceid)
        {
            string extension = "";

            ShareModel model = new ShareModel();
            try
            {
                using (var db = new ConquerorHubEntities())

                {
                    var datatype = db.CH_ParticipantRegistration.Where(x => x.ParticipantsPostId == id).Select(x => x.ContentType).FirstOrDefault();
                    if (datatype != null)
                    {
                        extension = datatype.Split('.').Last();
                    }
                    var type = db.CH_ParticipantsGallery.Where(x => x.PostId == id).Select(x => x.ContentType).FirstOrDefault();
                    if (type != null)
                    {
                        extension = type.Split('.').Last();
                    }

                }

                if (sourceid == "")
                {
                    model.SourcePage = User.Identity.GetUserId();
                }
                else
                {
                    model.SourcePage = sourceid;
                }


                model.ContentType = extension;
                model.DestinationPage = User.Identity.GetUserId();
                model.PostId = id;
                var value = data.SaveShare(SessionToken, model);

                return Json(value, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        public Dictionary<int, string> MainPhotosofProfile(Guid? userid)
        {
            Dictionary<int, string> data1 = new Dictionary<int, string>();

            var result = new List<MainPhotosModel>();
            var result1 = new List<MainPhotosModel>();
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId(); ;
      
            if (userid != null)
            {
                result = data.GetMainPhotos(SessionToken).Entity.Where(x => x.UserId == userid.ToString()).Select(x => x).ToList();
               
            }
            else
            {
                result = data.GetMainPhotos(SessionToken).Entity.Where(x => x.UserId == id).Select(x => x).ToList();

            }
        
            var model = new MainPhotosModel();
            if (result.Count != 0)
            {

                var length1 = result.FirstOrDefault().BannerPicData.Length;
                var length2 = result.FirstOrDefault().ProfilePicData.Length;

                model.BannerPicData = new byte[length1];
                model.ProfilePicData = new byte[length2];
                model.BannerPicData = result.FirstOrDefault().BannerPicData;
                model.ProfilePicData = result.FirstOrDefault().ProfilePicData;
                System.Web.HttpContext.Current.Session["Logedinuserprofile"] = Imageget(data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == System.Web.HttpContext.Current.User.Identity.GetUserId()) == null ? null : data.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == System.Web.HttpContext.Current.User.Identity.GetUserId()).ProfilePicData);
                if (model != null)
                {
                    if (model.ProfilePicData.Length != 0)
                    {
                        Stream inputstream = new MemoryStream(model.ProfilePicData, 0, model.ProfilePicData.Length);
                        MemoryStream memoryStream = inputstream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputstream.CopyTo(memoryStream);
                        }

                        string imreBase64Data = Convert.ToBase64String(model.ProfilePicData);
                        string imgDataURL2 = string.Format("data:image2/png;base64,{0}", imreBase64Data);
                        ViewBag.profilepic = imgDataURL2;
                        data1.Add(1, imgDataURL2);

                    }
                    if (model.BannerPicData.Length != 0)
                    {
                        Stream inputstream = new MemoryStream(model.BannerPicData, 0, model.BannerPicData.Length);
                        MemoryStream memoryStream = inputstream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputstream.CopyTo(memoryStream);
                        }

                        string imreBase64Data = Convert.ToBase64String(model.BannerPicData);
                        string imgDataURL2 = string.Format("data:image2/png;base64,{0}", imreBase64Data);
                        ViewBag.bannerpic = imgDataURL2;
                        data1.Add(2, imgDataURL2);

                    }
                    ViewBag.bannerpic = null;
                }
            }
            ViewBag.bannerpic = null;
            return data1;
        }
        [HttpPost]
        public ActionResult MainPhotosofProfile(HttpPostedFileBase bannerPic, HttpPostedFileBase profilePic)
        {

            byte[] bytes1 = new byte[0];
            byte[] bytes2 = new byte[0];
            if (bannerPic != null)
            {
                using (BinaryReader br = new BinaryReader(bannerPic.InputStream))
                {
                    bytes1 = br.ReadBytes(bannerPic.ContentLength);


                }
            }
            if (profilePic != null)
            {
                using (BinaryReader br = new BinaryReader(profilePic.InputStream))
                {
                    bytes2 = br.ReadBytes(profilePic.ContentLength);
                }
            }

            MainPhotosModel model = new MainPhotosModel();
            model.PhotosId = Guid.NewGuid();
            model.UserId = User.Identity.GetUserId();
            model.Id = Guid.NewGuid();
            model.BannerPicData = bytes1;
            model.ProfilePicData = bytes2;
            try
            {
                var result = data.SaveMainPhotosofProfile(SessionToken, model);
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult Redirecttoaction(string userid)
        {
            ViewBag.UserId = userid;

            var user = UserManager.FindById(userid);
            try
            {
                if (user.Usertype == 1)
                {
                    return RedirectToAction("Home", "OrganizerBasicDetails", new { UserId = userid });

                }
                else
                {
                    return RedirectToAction("ParticipantHome", "Participants", new { userid = userid });
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        public ActionResult GetNotifications()
        {
            //var list = new List<CH_EventRegistrationFromOrganizer>();
            try
            {
                var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
                NotificationComponent NC = new NotificationComponent();
                Tuple<List<EventRegistrationfromOrganizerModel>, List<LikesModel>, List<ShareModel>, List<SubscribersModel>, List<CommentModel>, List<SubCommentModel>> list = NC.GetData(notificationRegisterTime);



                Session["LastUpdate"] = DateTime.Now;
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public async Task<ActionResult> GetNotificationpost(Guid? postid, string sourceId)
        {

            //From Sponsor Page
            var services = new OrganiserBasicDetailsServices();
            NotificationPostModel notificationPost = new NotificationPostModel();
            notificationPost.sourceID = sourceId;
            notificationPost.PostId = postid;
            var sponsorList = services.GetSponsorList(SessionToken, Guid.Parse(User.Identity.GetUserId())).Entity;
            if (sponsorList != null)
            {
                var notifiedSponsorPost = sponsorList.Where(x => x.ImageId == postid).Select(x => x.Image).FirstOrDefault();
                if (notifiedSponsorPost != null)
                {
                    notificationPost.Image = Imageget(notifiedSponsorPost);
                }
            }
            //From Gallery Page of Participants
            var galleryList = await services.GetParticipantGallerydata(SessionToken);
            if (galleryList != null)
            {
                var filteredGallery = galleryList.FirstOrDefault(x => x.PostId == postid);

                notificationPost.Image = filteredGallery == null ? null : Imageget(filteredGallery.ImageData);
                notificationPost.Video = filteredGallery == null ? null : filteredGallery.VideoData;

            }
            //Participation post
            var participantPost = await services.GetParticipantRegistration(SessionToken);
            if (participantPost != null)
            {
                var filteredParticiapnts = participantPost.FirstOrDefault(x => x.VideoId == postid && x.OrganizerId == User.Identity.GetUserId());
                var eventImageorVideo = await services.GetEventRegistrationImageVideo(SessionToken);
                if (filteredParticiapnts != null)
                {
                    var ImageorVideo = eventImageorVideo == null ? null : eventImageorVideo.Where(x => x.Imagevideo.EventIdImageorVideo == filteredParticiapnts.EventId).Select(x => x.Imagevideo.imageorvideo).FirstOrDefault();
                    if (ImageorVideo == 1)
                    {
                        notificationPost.Image = filteredParticiapnts == null ? null : Imageget(filteredParticiapnts.Data);
                    }
                    else if (ImageorVideo == 2)
                    {
                        notificationPost.Video = filteredParticiapnts == null ? null : filteredParticiapnts.Data;
                    }
                }
            }
            //Home post
            var homeList = await services.GetHomePage(SessionToken);
            if (homeList != null)
            {
                notificationPost.PostId = postid;
                var homePost = homeList.FirstOrDefault(x => x.Postid == postid && x.UserId == User.Identity.GetUserId());
                if (homePost != null)
                {
                    if (homePost.Image != null)
                    {
                        notificationPost.Image = Imageget(homePost.Image);
                    }
                    if (homePost.Video != null)
                    {
                        notificationPost.Video = homePost.Video;
                    }
                }
            }
            ViewBag.LikeCount = data.GetTotalLikesOfPost(SessionToken).Entity == null ? 0 : data.GetTotalLikesOfPost(SessionToken).Entity.Where(x => x.Imageid == postid && x.DestinationUserId == User.Identity.GetUserId() && x.LikeStatus == true).Select(x => x).Count();
            ViewBag.DislikeCount = data.GetTotalLikesOfPost(SessionToken).Entity.Where(x => x.Imageid == postid && x.DestinationUserId == User.Identity.GetUserId() && x.LikeStatus == false).Select(x => x).Count();
            ViewBag.commentCount = data.GetcommentCount(SessionToken).Entity.Where(x => x.PostId == postid && x.DestinationUserId == User.Identity.GetUserId()).Select(x => x).Count();
            return View(notificationPost);


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
            else
            {
                return null;
            }
        }
        [HttpGet]
        public FileResult DownloadFile1(byte[] data)
        {

            var result = new CH_EventRegistrationFromOrganizer();
            using (var db = new ConquerorHubEntities())
            {
                result = db.CH_EventRegistrationFromOrganizer.ToList().Find(x => x.EventVideo == data);
            }
            return File(result.EventVideo, "video");
        }
        [HttpPost]
        public ActionResult DeleteSponsorPost(string Postid, string Userid)
        {
            try
            {

                var result = data.DeleteSponsorPhoto(SessionToken, Guid.Parse(Postid), Guid.Parse(Userid));
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        [HttpPost]
        public ActionResult DeleteHomePost(string Postid, string Userid)
        {
            try
            {

                var result = data.DeleteHomePost(SessionToken, Guid.Parse(Postid), Guid.Parse(Userid));
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        [HttpPost]
        public ActionResult DeleteSharePost(string Postid, string Userid)
        {
            try
            {
                var result = data.DeleteSharePost(SessionToken, Guid.Parse(Postid), Guid.Parse(Userid));
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public ActionResult DeletePaticipantsGalleryPost(string Postid, string Userid)
        {

            try
            {
                var result = data.DeleteParticipantsGalleryPost(SessionToken, Guid.Parse(Postid), Guid.Parse(Userid));
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }
        [HttpPost]
        public ActionResult PostBlockAccount(Guid? blockedUser, bool status)
        {
            BlockModel blockModel = new BlockModel();
            blockModel.BlockedUser = blockedUser.ToString();
            blockModel.BlockingUser = User.Identity.GetUserId();
            blockModel.DateTime = DateTime.UtcNow;
            blockModel.Status = status;

            data.SaveBlockUser(SessionToken, blockModel);

            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public ActionResult GetBlockUser(Guid? Id)

        {

            BlockModel blockModel = new BlockModel();
            blockModel.BlockedUser = Id.ToString();
            blockModel.BlockingUser = System.Web.HttpContext.Current.User.Identity.GetUserId();

            try
            {

                var result = data.GetBlockUser(SessionToken, blockModel);
                if (result.Entity.BlockedUser != null)
                {
                    var status = result.Entity.Status == null ? null : result.Entity.Status;
                    System.Web.HttpContext.Current.Session["BlockStatus"] = status;
                    return Json(new { status }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    System.Web.HttpContext.Current.Session["BlockStatus"] = false;
                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }



    }
}


































