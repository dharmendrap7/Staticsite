using Conquerorhub.Models;
using Conquerorhub.SDK.Services;
using Staticsite.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.IO;

using System.Data.Entity;
using System.Threading.Tasks;
using System.Text;
using Conquerorhub.SDK.EntityModel;

namespace Staticsite.Controllers
{
    [SessionTimeOut]
    public class EventsController : BaseController
    {


        // GET: Events
        public ActionResult OrganizeEvent(Guid? EventId, Guid? Userid)
        {
            try
            {
                if (Userid != null)
                {
                    ViewBag.UserId = Userid;
                }
                tabopen tabOpen = (tabopen)TempData["opentab"];
                if (tabOpen != null)
                {
                    ViewBag.AboutParticipantopen = tabOpen.AboutEvent;
                    ViewBag.ImportantDateandtime = tabOpen.AboutParticipant;
                    ViewBag.AwardReward = tabOpen.Imortantdates;
                    ViewBag.Default = tabOpen.Default;
                    ViewBag.EventImageVideo = tabOpen.Awarcreward;
                    ViewBag.PaymentGateway = tabOpen.EventVideoImage;
                    
                }
                EventRegistrationfromOrganizerModel obj = new EventRegistrationfromOrganizerModel();
                obj.aboutEvent = new AboutEvent();
                obj.aboutParticipants = new AboutParticipants();
                obj.awardReward = new AwardsAndRewards();
                obj.importantDates = new ImportantDates();
                EventsServices serviceObj = new EventsServices();
                obj.aboutEvent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.Where(x => x.EventId == EventId).Select(x => x).FirstOrDefault();
                if (EventId != null)
                {



                    obj.aboutParticipants.EventId = (Guid)EventId;

                    obj.awardReward.EventId = (Guid)EventId;

                    obj.importantDates.EventId = (Guid)EventId; ;
                }




                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        [HttpPost]
        public async Task<ActionResult> OrganizeEvent(EventRegistrationfromOrganizerModel model, string status)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            try
            {
                OrganiserBasicDetailsServices services = new OrganiserBasicDetailsServices();
                var eventid = Guid.Empty;
                EventsServices obj = new EventsServices();
                tabopen openTab = new tabopen();
                if (model.aboutEvent != null)
                {
                    if (model.aboutEvent.NameofOrganizer != null)
                    {
                        model.aboutEvent.EventId = Guid.NewGuid();

                        model.aboutEvent.OrganizerId = User.Identity.GetUserId();
                        Session["Eventid"] = model.aboutEvent.EventId;

                        var AboutEventTab = obj.SaveRegistrationAboutEvent(SessionToken, model).Entity;
                        openTab.Default = false;
                        openTab.AboutEvent = true;
                        ViewBag.AboutEventTab = AboutEventTab.Result;
                        eventid = AboutEventTab.Result;
                    }
                }
                if (model.aboutParticipants != null)
                {
                    model.aboutParticipants.OrganizerId = User.Identity.GetUserId();
                    model.aboutParticipants.EventId = (Guid)Session["Eventid"];

                    var eventParticipant = obj.SaveORegistrtionAboutParticipant(SessionToken, model).Entity;
                    openTab.AboutParticipant = true;
                    openTab.Default = false;
                    ViewBag.EvantParticipanttab = eventParticipant.Result;
                    eventid = eventParticipant.Result;
                }
                if (model.importantDates != null)
                {
                    model.importantDates.OrganizerId = User.Identity.GetUserId(); ;
                    model.importantDates.EventId = (Guid)Session["Eventid"];
                    var importantDates = obj.SaveImportantDateAndTime(SessionToken, model);
                    ViewBag.ImportantDates = importantDates.Entity.Result;
                    openTab.Default = false;
                    openTab.Imortantdates = true;
                    eventid = importantDates.Entity.Result;

                }
                if (model.awardReward != null)
                {
                    model.awardReward.OrganizerId = User.Identity.GetUserId(); ;


                    model.awardReward.EventId = (Guid)Session["Eventid"];
                    var AwardReward = obj.SaveAwardAndReward(SessionToken, model).Entity;
                    ViewBag.AwardandReward = AwardReward.Result;
                    openTab.Default = false;
                    openTab.Awarcreward = true;
                    eventid = AwardReward.Result;

                }
                if (status != null)
                {
                    using (var db = new ConquerorHubEntities())
                    {
                        CH_EventRegistrationFromOrganizer data = db.CH_EventRegistrationFromOrganizer.Where(x => x.OrganizerId == model.Imagevideo.OrganizerId && x.EventId == model.Imagevideo.EventIdImageorVideo).Select(x => x).FirstOrDefault();
                        if (status == "yes")
                        {
                            data.Eventdisplayonhomepage = true;
                        }
                        else
                        {
                            data.Eventdisplayonhomepage = false;
                        }
                    }

                }
                if (model.Imagevideo != null)
                {
                    model.Imagevideo.OrganizerId = User.Identity.GetUserId();
                    model.Imagevideo.EventIdImageorVideo = (Guid)Session["Eventid"];
                    openTab.EventVideoImage = true;
                    //openTab.AvoidDefault = true;
                    var user1 = await services.GetEventRegistrationImageVideo(SessionToken);
                    var user = user1.Where(x => x.Imagevideo.OrganizerId == model.Imagevideo.OrganizerId && x.Imagevideo.EventIdImageorVideo == model.Imagevideo.EventIdImageorVideo).Count();

                    if (model.Imagevideo.PostedImage != null)
                    {

                        MemoryStream target1 = new MemoryStream();
                        model.Imagevideo.PostedImage.InputStream.CopyTo(target1);
                        byte[] Photo = target1.ToArray();
                        model.Imagevideo.EventImage = Photo;
                        target1.Close();




                    }
                    if (user == 1)
                    {
                        byte[] bytes1;
                        using (BinaryReader br = new BinaryReader(model.Imagevideo.PostedVideo.InputStream))
                        {
                            bytes1 = br.ReadBytes(model.Imagevideo.PostedVideo.ContentLength);
                        }
                        var data = user1.Where(x => x.Imagevideo.OrganizerId == model.Imagevideo.OrganizerId && x.Imagevideo.EventIdImageorVideo == model.Imagevideo.EventIdImageorVideo).Select(x => x).FirstOrDefault();
                        EventRegistrationfromOrganizerModel detail = new EventRegistrationfromOrganizerModel();
                        detail.Imagevideo = new EventsImageandVideo();
                        detail.Imagevideo.EventImage = model.Imagevideo.EventImage;
                        detail.Imagevideo.EventVideo = bytes1;
                        detail.Imagevideo.EventIdImageorVideo = model.Imagevideo.EventIdImageorVideo;
                        detail.Imagevideo.OrganizerId = model.Imagevideo.OrganizerId;
                        await services.SaveEventRegistrationImageVideo(SessionToken, detail);

                    }
                }




                TempData["opentab"] = openTab;

                return RedirectToAction("OrganizeEvent", new { eventid = Session["Eventid"] });
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }

        public ActionResult FutureEvents(Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }

            EventRegistrationfromOrganizerModel obj = new EventRegistrationfromOrganizerModel();
            obj.aboutEventlist = new List<AboutEvent>();
            obj.aboutParticipantslist = new List<AboutParticipants>();
            obj.importantDateslist = new List<ImportantDates>();
            obj.awardRewardlist = new List<AwardsAndRewards>();
            EventsServices serviceObj = new EventsServices();
            try
            {
                var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.Where(x => x.EventStatus == 1);
                var Aboutparticipant = serviceObj.GetORegistrtionAboutParticipant(SessionToken, Userid).Entity.Where(x => x.EventStatus == 1).ToList();
                var AwardReward = serviceObj.GetAwardAndReward(SessionToken, Userid).Entity.Where(x => x.EventStatus == 1).ToList();
                var impdates = serviceObj.GetImportantDateAndTime(SessionToken, Userid).Entity.Where(x => x.EventStatus == 1).ToList();
                obj.aboutEventlist.AddRange(Aboutevent);
                obj.aboutParticipantslist.AddRange(Aboutparticipant);
                obj.importantDateslist.AddRange(impdates);
                obj.awardRewardlist.AddRange(AwardReward);
                if (obj == null)
                {
                    return View();
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public JsonResult GetSubEvents(string name)
        {
            List<SelectListItem> licities = new List<SelectListItem>();
            EventsServices serviceObj = new EventsServices();
            int id = (int)Enum.Parse(typeof(TypeofEvent), name, true);
            try
            {
                var result = serviceObj.Geteventsubtype(SessionToken).Entity.Where(x => x.EventReferenceId == id).Select(x => x).ToList();
                licities.Add(new SelectListItem { Text = "-Select-", Value = "0" });
                if (result != null)
                {
                    foreach (var x in result)
                    {
                        licities.Add(new SelectListItem { Text = x.EventSubType, Value = x.Id.ToString() });
                    }
                }
                return Json(licities, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

            // return Json(new SelectList(licities, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public async Task<ActionResult> PastEventsPerformance(Guid? Userid, Guid? EventId)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            else
            {
                Userid = Guid.Parse(User.Identity.GetUserId());
            }

            ParticipantsDetailModel model = new ParticipantsDetailModel();
            model.Participantregistration = new List<ParticipationRegistrationModel>();
            var data1 = new BasicFunctionalityofentireappService();
            var result = data1.GetTotalVoteperPost(SessionToken).Entity;
            model.voterslist = new List<VotesModel>();


            model.aboutEventlist = new List<AboutEvent>();
            model.aboutParticipantslist = new List<AboutParticipants>();
            model.importantDateslist = new List<ImportantDates>();
            model.awardRewardlist = new List<AwardsAndRewards>();
            EventsServices serviceObj = new EventsServices();
            try
            {
                var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.Where(x => x.EventStatus == 5).ToList();
                var Aboutparticipant = serviceObj.GetORegistrtionAboutParticipant(SessionToken, Userid).Entity.Where(x => x.EventStatus == 5).ToList();
                var AwardReward = serviceObj.GetAwardAndReward(SessionToken, Userid).Entity.Where(x => x.EventStatus == 5).ToList();
                var impdates = serviceObj.GetImportantDateAndTime(SessionToken, Userid).Entity.Where(x => x.EventStatus == 5).ToList();
                model.aboutEventlist.AddRange(Aboutevent);
                model.aboutParticipantslist.AddRange(Aboutparticipant);
                model.importantDateslist.AddRange(impdates);
                model.awardRewardlist.AddRange(AwardReward);
                var eventid = Aboutevent.Select(x => x.EventId);
                OrganiserBasicDetailsServices obj1 = new OrganiserBasicDetailsServices();
                var data = new List<ParticipationRegistrationModel>();
                var data2 = await obj1.GetParticipantRegistration(SessionToken);
                data.AddRange(data2.Where(x => x.OrganizerId == Userid.ToString() && x.EventId == EventId).Select(x => x).ToList());
                var ImageorVideo1 = await obj1.GetEventRegistrationImageVideo(SessionToken);

                var ImageorVideo = ImageorVideo1.Where(x => x.Imagevideo.EventIdImageorVideo == EventId).Select(x => x.Imagevideo.imageorvideo).FirstOrDefault();

                model.Participantregistration = new List<ParticipationRegistrationModel>();
                if (data.Count() != 0) {
                    foreach (var item in data)
                    {

                        model.Participantregistration.Add(new ParticipationRegistrationModel()
                        {
                            Data = ImageorVideo == 2 ? item.Data : null,
                            Images = ImageorVideo == 1 ? Imageget(item.Data) : null,
                            OrganizerId = item.OrganizerId,
                            ParticipantId = item.ParticipantId,
                            EventId = item.EventId,
                            VideoId = item.VideoId,
                            Name = item.Name,
                            Qualification = item.Qualification,
                            CollegeorSchool = item.CollegeorSchool,
                            ContentType = item.ContentType,


                        });
                    }


                }



                return View(model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }


        public async Task<ActionResult> EventDetailsToParticipants(Guid? id, Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            else
            {
                Userid = Guid.Parse(User.Identity.GetUserId());
            }
            EventRegistrationfromOrganizerModel obj = new EventRegistrationfromOrganizerModel();
            obj.Imagevideo = new EventsImageandVideo();
            OrganiserBasicDetailsServices service = new OrganiserBasicDetailsServices();


            var ImageorVideo1 = await service.GetEventRegistrationImageVideo(SessionToken);
            var data = ImageorVideo1.Where(x => x.Imagevideo.EventIdImageorVideo == id).Select(x => x).FirstOrDefault();

            try
            {

                if (data.Imagevideo.EventImage != null)
                {
                    obj.Imagevideo.Image = Imageget(data.Imagevideo.EventImage);

                }
                if (data.Imagevideo.EventVideo != null)
                {
                    obj.Imagevideo.EventVideo = data.Imagevideo.EventVideo;
                    obj.Imagevideo.EventIdImageorVideo = id;
                }
                ViewBag.Organizerid = data.Imagevideo.OrganizerId;


                obj.aboutEvent = new AboutEvent();
                obj.aboutParticipants = new AboutParticipants();
                obj.importantDates = new ImportantDates();
                obj.awardReward = new AwardsAndRewards();

                EventsServices serviceObj = new EventsServices();
                var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.Where(x => x.EventId == id).Select(x => x).FirstOrDefault();
                var result = serviceObj.Geteventsubtype(SessionToken).Entity.Where(x => x.Id == Aboutevent.SubTypeOfEvent).Select(x => x) == null ? null : serviceObj.Geteventsubtype(SessionToken).Entity.FirstOrDefault(x => x.Id == Aboutevent.SubTypeOfEvent).EventSubType;
                Aboutevent.SubTypeOfEventstring = result;
                var Aboutparticipant = serviceObj.GetORegistrtionAboutParticipant(SessionToken, Userid).Entity.Where(x => x.EventId == id).Select(x => x).FirstOrDefault();
                var AwardReward = serviceObj.GetAwardAndReward(SessionToken, Userid).Entity.Where(x => x.EventId == id).Select(x => x).FirstOrDefault();
                var impdates = serviceObj.GetImportantDateAndTime(SessionToken, Userid).Entity.Where(x => x.EventId == id).Select(x => x).FirstOrDefault();
                obj.aboutEvent = Aboutevent;
                obj.aboutParticipants = Aboutparticipant;
                obj.importantDates = impdates;
                obj.awardReward = AwardReward;


                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public async Task<ActionResult> EventsPage(Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }

            else
            {
                Userid = Guid.Parse(User.Identity.GetUserId());
            }

            EventRegistrationfromOrganizerModel obj = new EventRegistrationfromOrganizerModel();
            obj.aboutEventlist = new List<AboutEvent>();
            obj.aboutParticipantslist = new List<AboutParticipants>();
            obj.importantDateslist = new List<ImportantDates>();
            obj.awardRewardlist = new List<AwardsAndRewards>();
            EventsServices serviceObj = new EventsServices();
            OrganiserBasicDetailsServices services = new OrganiserBasicDetailsServices();
            var eventpost = await services.GetEventRegistrationImageVideo(SessionToken);
            obj.ImagevideoList = new List<EventsImageandVideo>();
            var basicfunctionality = new BasicFunctionalityofentireappService();
            var aspnetUser = new ApplicationMandatoryService();
            var getSubscribeuser = new List<EventRegistrationfromOrganizerModel>();

            if (eventpost.Count != 0)
            {

                foreach (var item in eventpost)
                {
                    obj.ImagevideoList.Add(new EventsImageandVideo()
                    {
                        EventIdImageorVideo = item.Imagevideo.EventIdImageorVideo,
                        OrganizerId = item.Imagevideo.OrganizerId,
                        Image = item.Imagevideo.EventImage != null ? Imageget(item.Imagevideo.EventImage) : null,
                        UserName = aspnetUser.Getuserlist(SessionToken).Entity.FirstOrDefault(x => x.Id == item.Imagevideo.OrganizerId).UserName,
                        Profilepic = Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item.Imagevideo.OrganizerId).ProfilePicData)

                    });
                }


            }
            try
            {
                var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.ToList();
                var Aboutparticipant = serviceObj.GetORegistrtionAboutParticipant(SessionToken, Userid).Entity.ToList();
                var AwardReward = serviceObj.GetAwardAndReward(SessionToken, Userid).Entity.ToList();
                var impdates = serviceObj.GetImportantDateAndTime(SessionToken, Userid).Entity.ToList();


                obj.aboutEventlist.AddRange(Aboutevent);
                obj.aboutParticipantslist.AddRange(Aboutparticipant);
                obj.importantDateslist.AddRange(impdates);
                obj.awardRewardlist.AddRange(AwardReward);
                if (obj == null)
                {
                    return View();
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public ActionResult CheckOTPRedirecttovideoupload(Guid? Eventid, Guid? OrganizerId)
        {
            if (OrganizerId != null)
            {
                ViewBag.UserId = OrganizerId;
            }
            OTPVerificationModel obj = new OTPVerificationModel();
            obj.EventId = Eventid;
            obj.OrganizerId = OrganizerId.ToString();

            return View(obj);
        }
        [HttpPost]
        public ActionResult CheckOTPRedirecttovideoupload(OTPVerificationModel model)
        {
            var result = Otpcheck(model.EventId, (int)model.OTP);
            if (result == "OTP sent")
            {
                ViewBag.OTPSent = "Please check your mail OTP is sent save it for future use";
                return RedirectToAction("CheckOTPRedirecttovideoupload");
            }
            else if (result == "OTP Deleted")
            {
                return RedirectToAction("Uploadvideo", new { Eventid = model.EventId, OrganizerId = model.OrganizerId });
            }

            return View();
        }
        public ActionResult Uploadvideo(Guid? Eventid, Guid? OrganizerId)
        {
            if (OrganizerId != null)
            {
                ViewBag.UserId = OrganizerId;
            }

            try
            {
                
                EventsServices serviceObj = new EventsServices();
                var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, OrganizerId).Entity.Where(x => x.EventId == Eventid).Select(x => x.ImageorVideo).FirstOrDefault();
                ViewBag.ImageorVideo = Aboutevent;
                ParticipationRegistrationModel obj = new ParticipationRegistrationModel();
                obj.EventId = Eventid;
                obj.OrganizerId = OrganizerId.ToString();
                return View(obj);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }

        public dynamic Otpcheck(Guid? EventId, int OTP)
        {

            var userid = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ParticipantServices obj1 = new ParticipantServices();
            var emailid = System.Web.HttpContext.Current.Session["EmailId"] as string;
            var Data = obj1.GetParticipantsregistration(SessionToken, (Guid)EventId).Entity.FirstOrDefault();
            var otpdb = obj1.Getotplist(SessionToken, (Guid)EventId).Entity.FirstOrDefault(x => x.UserId == userid) == null ? null : obj1.Getotplist(SessionToken, (Guid)EventId).Entity.FirstOrDefault(x => x.UserId == userid).OTP;
            if (OTP == 0)
            {
                if (otpdb == null)
                {

                    if (Data.Data == null)
                    {
                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                        string sRandomOTP = GenerateRandomOTP(6, saAllowedCharacters);

                        var model = new OTPVerificationModel()
                        {
                            UserId = userid,
                            EventId = EventId,
                            OTP = Convert.ToInt32(sRandomOTP),

                        };
                        obj1.SaveOTp(SessionToken, model);
                        MailMessage mail = new MailMessage();
                        mail.To.Add(emailid.ToString());
                        mail.From = new MailAddress("Premamnp@conquerorhub.com");
                        mail.Subject = "regarding OTP generated"; ;
                        mail.Body = "Please Keep the OTP generated for future use while uploading your performance" + sRandomOTP; ;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("premalth01@gmail.com", "PREMA44LATHA");
                        smtp.Send(mail);

                        return "OTP sent";
                    }
                }
            }

            if (otpdb != null)
            {
                if (otpdb == OTP)
                {
                    var model = new OTPVerificationModel()
                    {
                        UserId = userid,
                        EventId = EventId,
                        OTP = OTP,

                    };
                    obj1.DeleteOtp(SessionToken, model);
                    return "OTP Deleted";
                }
            }
            return "Error";
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)

        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        public async Task<ActionResult> RegisteredParticipantsfortheevent(Guid EventId)
        {

            ParticipantsDetailModel model = new ParticipantsDetailModel();
            model.Participantregistration = new List<ParticipationRegistrationModel>();
            OrganiserBasicDetailsServices obj = new OrganiserBasicDetailsServices();

            var Eventdetails1 = await obj.GetEventRegistrationImageVideo(SessionToken);

            var Eventdetails = Eventdetails1.Where(x => x.Imagevideo.EventIdImageorVideo == EventId).Select(x => x).FirstOrDefault();
            ViewBag.EventMode = Eventdetails.Imagevideo.EventMode;
            var data1 = await obj.GetParticipantRegistration(SessionToken);
            var data = data1.Where(x => x.EventId == EventId).Select(x => x).ToList();
            try
            {

                foreach (var item in data)
                {
                    model.Participantregistration.Add(new ParticipationRegistrationModel()
                    {
                        ParticipantId = item.ParticipantId,
                        OrganizerId = item.OrganizerId,
                        EventId = item.EventId,
                        CollegeorSchool = item.CollegeorSchool,
                        ContentType = item.ContentType,
                        About_Self = item.About_Self,
                       
                        Data = Eventdetails.Imagevideo.imageorvideo == 2 && item.Data != null ? item.Data : null,
                        Images = Eventdetails.Imagevideo.imageorvideo == 1 && item.Data != null ? Imageget(item.Data) : null,
                        ContactNumber = item.ContactNumber,
                        FileName = item.FileName,
                        Modeofcompitetion = (modeofcompetation)item.Modeofcompitetion,
                        VideoId = item.VideoId,
                        Name = item.Name,
                        Qualification = item.Qualification,




                    });
                }



                return View(model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveOngoingEventParticipant(ParticipationRegistrationModel model)
        {
            model.ParticipantId = User.Identity.GetUserId();

            OrganiserBasicDetailsServices obj = new OrganiserBasicDetailsServices();
         string data=  await obj.SavePerformanceFromparticipants(SessionToken, model);
            TempData["msg"] = data;

            return RedirectToAction("Uploadvideo", new { Eventid = model.EventId, OrganizerId = model.OrganizerId });
        }
        [HttpGet]
        public async Task<FileResult> DownloadFile1(Guid? EventId)
        {

            var basicfunctionality = new OrganiserBasicDetailsServices();
            var result = new List<EventRegistrationfromOrganizerModel>();

            try
            {
                result = await basicfunctionality.GetEventRegistrationImageVideo(SessionToken);
                var data = result.ToList().Find(x => x.Imagevideo.EventIdImageorVideo == EventId);

                return File(data.Imagevideo.EventVideo, "Video", "Video");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<FileResult> DownloadFile(Guid? VideoID, Guid? EventId)
        {
            var basicfunctionality = new OrganiserBasicDetailsServices();
            var result = new List<ParticipationRegistrationModel>();

            try
            {
                result = await basicfunctionality.GetParticipantRegistration(SessionToken);
                var data = result.ToList().Find(x => x.VideoId == VideoID);

                return File(data.Data, data.ContentType, data.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult UpdateParticipantStatus(String Status, Guid? Eventid, string UserId)
        {

            var obj = new ParticipantServices();
            var email = UserManager.FindById(UserId).Email;
            ParticipationRegistrationModel data = new ParticipationRegistrationModel();
            if (Status == "Accept")
            {
                data.ParticipantStatus = 3;
                data.EventId = Eventid;
                data.OrganizerId = User.Identity.GetUserId();
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("Premamn@conquerorhub.com");
                mail.Subject = "Regarding Performance Status"; ;
                mail.Body = "Your performance is selected to next level of competition";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("premalth01@gmail.com", "PREMA44LATHA");
                smtp.Send(mail);
            }
            else if (Status == "Reject")
            {
                data.ParticipantStatus = 4;
                data.EventId = Eventid;
                data.OrganizerId = User.Identity.GetUserId();
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("Premamn@conquerorhub.com");
                mail.Subject = "Regarding Performance Status"; ;
                mail.Body = "Your performance is rejected,Don't be disheartened.BETTER LUCK NEXT TIME";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("premalth01@gmail.com", "PREMA44LATHA");
                smtp.Send(mail);
            }
            try
            {
                var result = obj.UpdatePArticipantStatus(SessionToken, data);

                return RedirectToAction("RegisteredParticipantsfortheevent", new { EventId = Eventid });
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }
        }
        public async Task<ActionResult> ShortlistedParticipants(Guid? Eventid, Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            ParticipantsDetailModel model = new ParticipantsDetailModel();
            model.Participantregistration = new List<ParticipationRegistrationModel>();
            var data1 = new BasicFunctionalityofentireappService();
            var result = data1.GetTotalVoteperPost(SessionToken).Entity;
            model.voterslist = new List<VotesModel>();
            model.voterslist = result;
            Random rnd = new Random();
            OrganiserBasicDetailsServices service = new OrganiserBasicDetailsServices();
            var participantRegistration = await service.GetParticipantRegistration(SessionToken);
            var data = participantRegistration.Where(x => x.ContentType != null && x.EventId == Eventid && x.ParticipantStatus == 3).Select(x => x).OrderBy(x => Guid.NewGuid()).ToList();
            var ImageorVideo1 = await service.GetEventRegistrationImageVideo(SessionToken);
            var ImageorVideo = ImageorVideo1.Where(x => x.Imagevideo.EventIdImageorVideo == Eventid).Select(x => x.Imagevideo.imageorvideo).FirstOrDefault();
            try
            {

                foreach (var item in data)
                {
                    model.Participantregistration.Add(new ParticipationRegistrationModel()
                    {
                        OrganizerId = item.OrganizerId,
                        EventId = item.EventId,
                        CollegeorSchool = item.CollegeorSchool,
                        ContentType = item.ContentType,
                        About_Self = item.About_Self,
                        DateOfBirthh = item.DateOfBirthh,
                        Data = ImageorVideo == 2 ? item.Data : null,
                        Images = ImageorVideo == 1 ? Imageget(item.Data) : null,
                        ContactNumber = item.ContactNumber,
                        FileName = item.FileName,
                        Modeofcompitetion = (modeofcompetation)item.Modeofcompitetion,
                        VideoId = item.VideoId,
                        Name = item.Name,
                        Qualification = item.Qualification,




                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

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

        public JsonResult Searchbox()
        {
            var data1 = new List<AspnetUsersModel>();
            List<string> EmailList = new List<string>();
            try
            {
                var data11 = new ConquerorHubEntities().AspNetUsers.Select(x => new AspnetUsersModel() { Id = x.Id, Email = x.Email, UserName = x.UserName, UserActive = x.UserActive }).ToList();
                Session["UserData"] = data11;
                foreach (var i in data11)
                {
                    EmailList.Add(i.Email);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(EmailList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HallofFame(Guid? Userid)
        {
            if (Userid != null)
            {
                ViewBag.UserId = Userid;
            }
            try
            {
                ParticipantsDetailModel model = new ParticipantsDetailModel();
                model.Participantregistration = new List<ParticipationRegistrationModel>();
                var data1 = new BasicFunctionalityofentireappService();
                var result = data1.GetTotalVoteperPost(SessionToken).Entity;
                model.voterslist = new List<VotesModel>();
                model.voterslist = result;

                using (var db = new ConquerorHubEntities())

                {
                    Dictionary<int, Guid?> dict = new Dictionary<int, Guid?>();
                    var data4 = db.CH_ParticipantRegistration.Where(f => f.ParticipantId == Userid.ToString()).Select(f => f).ToList();
                    foreach (var item in data4)
                    {
                        var count = db.CH_VoteTable.GroupBy(a => a.EventId == item.EventId && a.PostId == item.ParticipantsPostId && a.VoteStatus == true).Select(a => a).Count();
                        if (count != 0)
                        {
                            dict.Add(count, item.ParticipantsPostId);
                            dict.OrderByDescending(x => x.Key);
                        }

                    }

                    if (dict.Count != 0)
                    {
                        var values = dict.Take(3).ToDictionary(x => x.Value).Values;

                        var data01 = new List<ParticipationRegistrationModel>();

                        var data0 = new List<CH_ParticipantRegistration>();
                        foreach (var item in values)
                        {
                            data0 = db.CH_ParticipantRegistration.Where(d => d.ParticipantsPostId == item.Value.Value).Select(d => d).ToList();
                        }

                        foreach (var item in data0)
                        {
                            model.Participantregistration = new List<ParticipationRegistrationModel>();
                            model.Participantregistration.Add(new ParticipationRegistrationModel()
                            {
                                Data = item.Data,
                                OrganizerId = item.OrganizerId,
                                ParticipantId = item.ParticipantId,
                                EventId = item.EventId,
                                VideoId = item.ParticipantsPostId,
                                Name = item.Name,
                                Qualification = item.Qualification,
                                CollegeorSchool = item.CollegeorSchool,

                            });

                        }
                    }
                    model.aboutEventlist = new List<AboutEvent>();
                    model.aboutParticipantslist = new List<AboutParticipants>();
                    model.importantDateslist = new List<ImportantDates>();
                    model.awardRewardlist = new List<AwardsAndRewards>();
                    EventsServices serviceObj = new EventsServices();

                    var Aboutevent = serviceObj.GetRegistrationAboutEvent(SessionToken, Userid).Entity.ToList();
                    var Aboutparticipant = serviceObj.GetORegistrtionAboutParticipant(SessionToken, Userid).Entity.ToList();
                    var AwardReward = serviceObj.GetAwardAndReward(SessionToken, Userid).Entity.ToList();
                    var impdates = serviceObj.GetImportantDateAndTime(SessionToken, Userid).Entity.ToList();
                    model.aboutEventlist.AddRange(Aboutevent);
                    model.aboutParticipantslist.AddRange(Aboutparticipant);
                    model.importantDateslist.AddRange(impdates);
                    model.awardRewardlist.AddRange(AwardReward);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Errorpage/Errorpage.cshtml");
            }

        }

        public ActionResult EventDetails()
        {
            EventsServices serviceObj = new EventsServices();
            EventDetailsViewModel obj = new EventDetailsViewModel();
            obj.EventSubType = new List<string>();
            obj.eventDetails = new List<AboutEvent>();
            var Aboutevent1 = serviceObj.GetAllEvents(SessionToken).Entity.Where(x=>x.EventStatus==1).Select(x => x).ToList();
            foreach (var item in Aboutevent1)
            {
                obj.EventSubType.Add( serviceObj.Geteventsubtype(SessionToken).Entity.Where(x => x.Id == item.SubTypeOfEvent).Select(x => x) == null ? null : serviceObj.Geteventsubtype(SessionToken).Entity.FirstOrDefault(x => x.Id == item.SubTypeOfEvent).EventSubType);
            }


            obj.eventDetails.AddRange(Aboutevent1);

            return View(obj);
        }
    }
    }
