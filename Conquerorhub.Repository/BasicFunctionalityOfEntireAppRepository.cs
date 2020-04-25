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
   public class BasicFunctionalityOfEntireAppRepository:RepositoryBase
    {
        public RequestResult<List<LikesModel>> GetTotallikeofPost(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/gettotallikesofthepost/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<List<LikesModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<CommentModel>> GetCommentCount(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/getcommentlistcount/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<List<CommentModel>>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<SubscribersModel>> GettotalSubscribersperprofile(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/gettotalsubscribersperprofile/?sessionToken={sessionToken}";
            try
            {
                return GetAndParseData<RequestResult<List<SubscribersModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<CommentModel>> GettotalComment(string sessionToken,string postid)
        {
            var parameters = $"/savecommonfunctionality/getpostcomment/?sessionToken={sessionToken}&Postid={postid}";
            try
            {
                return GetAndParseData<RequestResult<List<CommentModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<SubCommentModel>> GetSubComment(string sessionToken,string Postid)
        {

            var parameters = $"/savecommonfunctionality/getpostSubcomment/?sessionToken={sessionToken}&Postid={Postid}";
            try
            {
                return GetAndParseData<RequestResult<List<SubCommentModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public RequestResult<List<VotesModel>> GetTotalvotersperpost(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/gettotalvotesperpost/?sessionToken={sessionToken}";
            try
            {
                return GetAndParseData<RequestResult<List<VotesModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<ShareModel>> GettotalShare(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/gettotalshare/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<List<ShareModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public RequestResult<List<BlockModel>> GetBlockList(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/getblockeduser/?sessionToken={sessionToken}";
            try
            {

                return GetAndParseData<RequestResult<List<BlockModel>>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<BlockModel> GetBlockUser(string sessionToken,BlockModel model)
        {
            var parameters = $"/savecommonfunctionality/getblockusers/?sessionToken={sessionToken}&&blockeduser={model.BlockedUser}&&blockingUser={model.BlockingUser}";
            try
            {

                return GetAndParseData<RequestResult<BlockModel>>(null, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<List<MainPhotosModel>> GetMainphotos(string sessionToken)
        {
            var parameters = $"/savecommonfunctionality/getmainphotos/?sessionToken={sessionToken}";
            try
            {
                return GetAndParseData<RequestResult<List<MainPhotosModel>>>(null, parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public RequestResult<IntResult> SaveLikes(string sessionToken, LikesModel model)
        {
            string parameters = $"/savecommonfunctionality/savelikes?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                var res=PostAndGetData<RequestResult<LikesModel>>(sessionToken, parameters, serilizedData);
                if(res!=null)
                {
                    //GetTotallikeofPost    hey how to get like count from result obj
                    var result = (GetTotallikeofPost(sessionToken).Entity).Where(a => a.DestinationUserId == model.DestinationUserId && a.Imageid == model.Imageid).Select(a => a);
                    int LikeCount = result.Where(a => a.LikeStatus == true).Count();
                    int disLikeCount = result.Where(a => a.LikeStatus == false).Count();
                   // int getLikeCount = (GetTotallikeofPost(sessionToken).Entity).Count>0 ? ((GetTotallikeofPost(sessionToken).Entity).Where(a => a.DestinationUserId == model.DestinationUserId && a.Imageid == model.Imageid).Select(a=>a).Count()): 0;
                    return new RequestResult<IntResult>(new IntResult(new int[2] { LikeCount,disLikeCount}));
                    
                }
                else
                    return new RequestResult<IntResult>(new IntResult(new int[2] { 0,0}));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<SubscribersModel> SaveSubscribers(string sessionToken, SubscribersModel model)
        {
            string parameters = $"/savecommonfunctionality/savesubscribers?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<SubscribersModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<VotesModel> SaveVoters(string sessionToken, VotesModel model)
        {
            string parameters = $"/savecommonfunctionality/savevote?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<VotesModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<CommentResponceModel> SaveComment(string sessionToken, CommentModel model)
        {
          
            string parameters = $"/savecommonfunctionality/savecomment?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<CommentResponceModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<CommentResponceModel> SaveSubComment(string sessionToken, SubCommentModel model)
        {
            string parameters = $"/savecommonfunctionality/savesubcomment?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<CommentResponceModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public RequestResult<ShareModel> SaveShare(string sessionToken, ShareModel model)
        {
            string parameters = $"/savecommonfunctionality/saveshare?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<ShareModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<BlockModel> SaveBlockUser(string sessionToken, BlockModel model)
        {
            string parameters = $"/savecommonfunctionality/saveblockusers?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<BlockModel>>(sessionToken, parameters, serilizedData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<MainPhotosModel> SaveMainPhotosofProfile(string sessionToken, MainPhotosModel model)
        {
            string parameters = $"/savecommonfunctionality/savemainphotos?sessionToken={sessionToken}";
            try
            {
                var serilizedData = JsonConvert.SerializeObject(model);
                return PostAndGetData<RequestResult<MainPhotosModel>>(sessionToken, parameters, serilizedData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public RequestResult<GuidResult> DeleteSponsorPost(string sessionToken, Guid?Postid,Guid?Userid)
        {
            string parameters = $"/savecommonfunctionality/deletesponsorspost?sessionToken={sessionToken}&postid={Postid}&Userid={Userid}";
            try
            {
                return PostAndGetData<RequestResult<GuidResult>>(sessionToken, parameters, "");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteHomePost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            string parameters = $"/savecommonfunctionality/deletehomepost?sessionToken={sessionToken}&postid={Postid}&Userid={Userid}";
            try
            {
                return PostAndGetData<RequestResult<GuidResult>>(sessionToken, parameters, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteSharePost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            string parameters = $"/savecommonfunctionality/deletesharepost?sessionToken={sessionToken}&postid={Postid}&Userid={Userid}";
            try
            {
                return PostAndGetData<RequestResult<GuidResult>>(sessionToken, parameters, "");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<GuidResult> DeleteParticipantsGalleryPost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            string parameters = $"/savecommonfunctionality/deleteparticipantsgallerypost?sessionToken={sessionToken}&postid={Postid}&Userid={Userid}";
            try
            {
                return PostAndGetData<RequestResult<GuidResult>>(sessionToken, parameters, "");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
