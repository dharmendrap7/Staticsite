using Conquerorhub.Models;
using Conquerorhub.SDK.EntityModel;
using Conquerorhub.SDK.Services;
using Microsoft.AspNet.SignalR;
using Staticsite.Controllers;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Staticsite.Controllers
{
    public class NotificationComponent:BaseController
    {
        public void RegisterNotification(DateTime? currentTime)

        {
            currentTime = System.Web.HttpContext.Current.Session["LastUpdated"] as DateTime?;
            var userId = System.Web.HttpContext.Current.Session["UserId"] as string;
            string conStr = ConfigurationManager.ConnectionStrings["ConquerorHubEntities2"].ConnectionString;
            string sqlCommand = @"SELECT [OrganizerId],[NameofOrganizer],[EventName] from [dbo].[CH_EventRegistrationFromOrganizer] where StartofEventRegistration >@StartofEventRegistration and OrganizerId=@UserID";
            string sqlCommand1 = @"SELECT [Id],[LikeStatus],[Imageid],[SourceUserId],[DestinationUserId] from [dbo].[CH_Likes] where DateandTime>@DateandTime and LikeStatus=@LikeStatus and DestinationUserId=@UserID";
            string sqlCommand2 = @"SELECT [Id],[PostId],[SourcePage],[DestinationPage] from [dbo].[CH_ShareTable] where DateTime>@DateTime and SourcePage=@UserID";
            string sqlCommand3 = @"SELECT [Id],[SubscriberUserId],[ProfileUserId],[SubscriptionStatus] from [dbo].[CH_SubscriptionTable] where Datetime>@Datetime and SubscriptionStatus=@SubscriptionStatus and ProfileUserId=@UserID";
            string sqlCommand4 = @"SELECT [Id],[PostId],[userid],[VoteStatus],[EventId] from [dbo].[CH_VoteTable] where DateTime>@Datetime and VoteStatus=@VoteStatus and userid=@UserID";
            string sqlCommand5 = @"SELECT [Id],[DestinationUserId],[SourceUserId],[PostId],[CommentMessage],[CommentedDate] from [dbo].[CH_CommentTable] where CommentedDate>@DateandTime and DestinationUserId=@UserID";
            string sqlCommand6 = @"SELECT [SubCommentId],[DestinationUserId],[SourceUserId],[PostId],[CommentId],[SubCommentDatetime] [SubCommentmsg] from [dbo].[CH_SubComment] where SubCommentDatetime>@DateandTime and DestinationUserId=@UserID";

            //string sqlCommand = @"SELECT [OrganizerId],[NameofOrganizer],[EventName] from [dbo].[CH_EventRegistrationFromOrganizer] where StartofEventRegistration >@StartofEventRegistration";
            //string sqlCommand1 = @"SELECT [Id],[LikeStatus],[Imageid],[SourceUserId],[DestinationUserId] from [dbo].[CH_Likes] where DateandTime>@DateandTime and LikeStatus=@LikeStatus";
            //string sqlCommand2 = @"SELECT [Id],[PostId],[SourcePage],[DestinationPage] from [dbo].[CH_ShareTable] where DateTime>@DateTime";
            //string sqlCommand3 = @"SELECT [Id],[SubscriberUserId],[ProfileUserId],[SubscriptionStatus] from [dbo].[CH_SubscriptionTable] where Datetime>@Datetime and SubscriptionStatus=@SubscriptionStatus";
            //string sqlCommand4 = @"SELECT [Id],[PostId],[userid],[VoteStatus],[EventId] from [dbo].[CH_VoteTable] where DateTime>@Datetime and VoteStatus=@VoteStatus and userid=@UserID";
            //string sqlCommand5 = @"SELECT [Id],[DestinationUserId],[SourceUserId],[PostId],[CommentMessage],[CommentedDate] from [dbo].[CH_CommentTable] where CommentedDate>@DateandTime";
            //string sqlCommand6 = @"SELECT [SubCommentId],[DestinationUserId],[SourceUserId],[PostId],[CommentId],[SubCommentDatetime] [SubCommentmsg] from [dbo].[CH_SubComment] where SubCommentDatetime>@DateandTime";
            //you can notice here I have added table name like this [dbo].[Contacts] with [dbo], its mendatory when you use Sql Dependency  
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);

                SqlCommand cmd1 = new SqlCommand(sqlCommand1, con);
                SqlCommand cmd2 = new SqlCommand(sqlCommand2, con);
                SqlCommand cmd3 = new SqlCommand(sqlCommand3, con);
                SqlCommand cmd4 = new SqlCommand(sqlCommand4, con);
                SqlCommand cmd5 = new SqlCommand(sqlCommand5, con);
                SqlCommand cmd6 = new SqlCommand(sqlCommand6, con);

                cmd.Parameters.AddRange(new[] { new SqlParameter("@StartofEventRegistration", currentTime),
                   new SqlParameter("@UserID", userId),
                  
                });
                cmd1.Parameters.AddRange(new[] { new SqlParameter("@DateandTime", currentTime),
                   new SqlParameter("@UserID", userId),
                   new SqlParameter("@LikeStatus", true),
                });
                cmd2.Parameters.AddRange(new[] { new SqlParameter("@DateTime", currentTime),
                   new SqlParameter("@UserID", userId),
                   
                });
                cmd3.Parameters.AddRange(new[] { new SqlParameter("@Datetime", currentTime),
                   new SqlParameter("@UserID", userId),
                   new SqlParameter("@SubscriptionStatus", true),
                });
                cmd4.Parameters.AddRange(new[] { new SqlParameter("@DateTime", currentTime),
                   new SqlParameter("@UserID", userId),
                   new SqlParameter("@VoteStatus", true),
                });
                cmd5.Parameters.AddRange(new[] { new SqlParameter("@DateandTime", currentTime),
                   new SqlParameter("@UserID", userId),
                   new SqlParameter("@VoteStatus", true),
                });
                cmd6.Parameters.AddRange(new[] { new SqlParameter("@DateandTime", currentTime),
                   new SqlParameter("@UserID", userId),
                   
                });

                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                cmd1.Notification = null;
                cmd2.Notification = null;
                cmd3.Notification = null;
                cmd4.Notification = null;
                cmd5.Notification = null;
                cmd6.Notification = null;

                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                SqlDependency.Start(conStr);
                //we must have to execute the command here  
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now  
                }

          SqlDependency sqlDep1 = new SqlDependency(cmd1);
                sqlDep1.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    // nothing need to add here now  
                }

                SqlDependency sqlDep2 = new SqlDependency(cmd2);
                sqlDep2.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    // nothing need to add here now  
                }

                SqlDependency sqlDep3 = new SqlDependency(cmd3);
                sqlDep3.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd3.ExecuteReader())
                {
                    // nothing need to add here now  
                }

                SqlDependency sqlDep4 = new SqlDependency(cmd4);
                sqlDep4.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd4.ExecuteReader())
                {
                    // nothing need to add here now  
                }

                SqlDependency sqlDep5 = new SqlDependency(cmd5);
                sqlDep5.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd5.ExecuteReader())
                {
                    // nothing need to add here now  
                }

                SqlDependency sqlDep6 = new SqlDependency(cmd6);
                sqlDep6.OnChange += sqlDep_OnChange;
                using (SqlDataReader reader = cmd6.ExecuteReader())
                {
                    // nothing need to add here now  
                }
            }
        }
        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record  
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client  
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");
                //re-register notification  
                RegisterNotification(DateTime.Now);
            }
            
        }
        public Tuple<List<EventRegistrationfromOrganizerModel>, List<LikesModel>, List<ShareModel>, List<SubscribersModel>,List<CommentModel>,List<SubCommentModel>> GetData(DateTime afterDate)
        {
            try
            {
                var basicfunctionality = new BasicFunctionalityofentireappService();
                var userId = System.Web.HttpContext.Current.Session["UserId"] as string;
                List<string> likepic = new List<string>();
                List<LikesModel> Like = new List<LikesModel>();
                List<string> sharepic = new List<string>();
                List<ShareModel> share = new List<ShareModel>();
                List<string> subscriberpic = new List<string>();
                List<SubscribersModel> subscriber = new List<SubscribersModel>();
                List<string> eventOpic = new List<string>();
                List<EventRegistrationfromOrganizerModel> Event = new List<EventRegistrationfromOrganizerModel>();
                List<string> commentPic = new List<string>();
                List<CommentModel> comment = new List<CommentModel>();
                List<string> subCommentPic = new List<string>();
                List<SubCommentModel> subComment = new List<SubCommentModel>();


                using (ConquerorHubEntities dc = new ConquerorHubEntities())
                {
                    
                    var likeid = dc.CH_Likes.Where(a => a.DateandTime > afterDate && a.DestinationUserId == userId && a.LikeStatus == true).Select(a => a.SourceUserId).ToList();
                    var shareid= dc.CH_ShareTable.Where(a => a.DateTime > afterDate && a.SourcePage == userId).OrderByDescending(a => a.DateTime).Select(x=>x.SourcePage);
                    var subscriberid = dc.CH_SubscriptionTable.Where(a => a.Datetime > afterDate && a.ProfileUserId == userId && a.SubscriptionStatus == true).OrderByDescending(a => a.Datetime).Select(a => a.ProfileUserId).ToList();
                    var subscribertoid = dc.CH_SubscriptionTable.Where(a => a.Datetime > afterDate && a.ProfileUserId == userId && a.SubscriptionStatus == true).OrderByDescending(a => a.Datetime).Select(a => a.SubscriberUserId).ToList();
                    var commentId = dc.CH_CommentTable.Where(a => a.CommentedDate > afterDate && a.DestinationUserId == userId).Select(a => a.SourceUserId).ToList();
                    var subCommentId = dc.CH_SubComment.Where(a => a.SubCommentDatetime > afterDate && a.DestinationUserId == userId).Select(a => a.SourceUserId).ToList();



                    foreach (var item in subscribertoid)
                    {
                        eventOpic.Add(Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData));

                    }
                    foreach (var item in subscribertoid)
                    {
                        Event.AddRange(from a in dc.CH_EventRegistrationFromOrganizer where (a.StartofEventRegistration > afterDate) select new EventRegistrationfromOrganizerModel() { aboutEvent = new AboutEvent() { EventId = a.EventId, NameofOrganizer = a.NameofOrganizer, Profilepic = item } });
                    }
              

                        foreach (var item in likeid)
                    {
                       likepic.Add(Imageget( basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData));


                }


                    
                    foreach (var item in commentId)
                    {
                        commentPic.Add(Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData));


                    }
                    foreach (var item in subCommentId)
                    {
                        subCommentPic.Add(Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData));


                    }
                    
                    foreach (var item in likepic)
                    {
                        Like.AddRange( dc.CH_Likes.Where(a => a.DateandTime > afterDate && a.DestinationUserId == userId && a.LikeStatus == true).OrderByDescending(a => a.DateandTime).Select(a => new LikesModel() { SourceUserId = a.SourceUserId, DestinationUserId = a.DestinationUserId, Imageid = a.Imageid, UserName = dc.AspNetUsers.Where(x => x.Id == a.SourceUserId).Select(x => x.UserName).FirstOrDefault(), profilepic = item }));
                    }
                    foreach (var item in commentPic)
                    {
                        comment.AddRange(dc.CH_CommentTable.Where(a => a.CommentedDate > afterDate && a.DestinationUserId == userId).OrderByDescending(a => a.CommentedDate).Select(a => new CommentModel() { SourceUserId = a.SourceUserId, DestinationUserId = a.DestinationUserId, PostId = a.PostId, UserName = dc.AspNetUsers.Where(x => x.Id == a.SourceUserId).Select(x => x.UserName).FirstOrDefault(), ProfilePic = item }));
                    }
                    foreach (var item in subCommentPic)
                    {
                        subComment.AddRange(dc.CH_SubComment.Where(a => a.SubCommentDatetime > afterDate && a.DestinationUserId == userId).OrderByDescending(a => a.SubCommentDatetime).Select(a => new SubCommentModel() { SourceUserId = a.SourceUserId, DestinationUserId = a.DestinationUserId, PostId = a.PostId, UserName = dc.AspNetUsers.Where(x => x.Id == a.SourceUserId).Select(x => x.UserName).FirstOrDefault(), ProfilePic = item }));
                    }

                    foreach (var item in shareid)
                    {
                        sharepic.Add(Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData));
                    }
                    foreach (var item in sharepic)
                    {
                        share.AddRange(dc.CH_ShareTable.Where(a => a.DateTime > afterDate && a.SourcePage == userId).OrderByDescending(a => a.DateTime).Select(a => new ShareModel() { PostId = a.PostId, UserName = dc.AspNetUsers.Where(x => x.Id == a.DestinationPage).Select(x => x.UserName).FirstOrDefault(), profilepic = item }).ToList());
                    }

                    foreach (var item in subscriberid)
                    {
                        subscriberpic.Add((Imageget(basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item) == null ? null : basicfunctionality.GetMainPhotos(SessionToken).Entity.FirstOrDefault(x => x.UserId == item).ProfilePicData)));
                    }
                        foreach (var item in subscriberpic)
                    {
                        subscriber.AddRange(dc.CH_SubscriptionTable.Where(a => a.Datetime > afterDate && a.ProfileUserId == userId && a.SubscriptionStatus == true).OrderByDescending(a => a.Datetime).Select(a => new SubscribersModel() { SubscriberUserId = a.SubscriberUserId, Subscribersname = dc.AspNetUsers.Where(x => x.Id == a.SubscriberUserId).Select(x => x.UserName).FirstOrDefault(), profilepic = item }).ToList());
                    }
                       
                    
                   // var votes = dc.CH_VoteTable.Where(a => a.DateTime >afterDate ).OrderByDescending(a => a.DateTime).Select(a => new VotesModel() { PostId = a.PostId, userid = a.userid }).ToList();
                    
                    var abc = new Tuple<List<EventRegistrationfromOrganizerModel>, List<LikesModel>, List<ShareModel>, List<SubscribersModel>,List<CommentModel>,List<SubCommentModel>>(Event, Like, share, subscriber,comment,subComment);
                    return abc;
                }
            }
            catch(Exception ex) {
                throw;
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
    }
   
}