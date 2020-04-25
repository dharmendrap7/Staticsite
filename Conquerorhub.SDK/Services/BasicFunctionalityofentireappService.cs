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
    public class BasicFunctionalityofentireappService
    {
        private readonly BasicFunctionalityOfEntireAppRepository _repository;

        public BasicFunctionalityofentireappService()
        {
            _repository = new BasicFunctionalityOfEntireAppRepository();
        }

        public RequestResult<List<LikesModel>> GetTotalLikesOfPost(string sessionToken)
        {
            try
            {
                return _repository.GetTotallikeofPost(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<List<CommentModel>> GetcommentCount(string sessionToken)
        {
            try
            {
                return _repository.GetCommentCount(sessionToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<CommentModel>> GetTotalComment(string sessionToken,string postid)
        {
            try
            {
                return _repository.GettotalComment(sessionToken, postid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<SubCommentModel>> GetSubComment(string sessionToken,string postid)
        {
            try
            {
                return _repository.GetSubComment(sessionToken, postid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<SubscribersModel>> GetTotalSubscribersperProfile(string sessionToken)
        {
            try
            {
                return _repository.GettotalSubscribersperprofile(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<VotesModel>> GetTotalVoteperPost(string sessionToken)
        {
            try
            {
                return _repository.GetTotalvotersperpost(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<ShareModel>> GetTotalShare(string sessionToken)
        {
            try
            {
                return _repository.GettotalShare(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<BlockModel>> GetBlockList(string sessionToken)
        {
            try
            {
                return _repository.GetBlockList(sessionToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<BlockModel> GetBlockUser(string sessionToken,BlockModel model)
        {
            try
            {
                return _repository.GetBlockUser(sessionToken,model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<IntResult> SaveLikes(string sessionToken, LikesModel model)
        {
            try
            {
                return _repository.SaveLikes(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<SubscribersModel> SaveSubscribers(string sessionToken, SubscribersModel model)
        {
            try
            {
                return _repository.SaveSubscribers(sessionToken, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<VotesModel> SaveVoters(string sessionToken, VotesModel model)
        {
            try
            {
                return _repository.SaveVoters(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<CommentResponceModel> Savecomment(string sessionToken, CommentModel model)
        {
            try
            {
                return _repository.SaveComment(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<CommentResponceModel> SaveSubcomment(string sessionToken, SubCommentModel model)
        {
            try
            {
                return _repository.SaveSubComment(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<ShareModel> SaveShare(string sessionToken, ShareModel model)
        {try
            {
                return _repository.SaveShare(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RequestResult<BlockModel> SaveBlockUser(string sessionToken, BlockModel model)
        {
            try
            {
                return _repository.SaveBlockUser(sessionToken, model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<MainPhotosModel> SaveMainPhotosofProfile(string sessionToken, MainPhotosModel model)
        {
            try
            {
                return _repository.SaveMainPhotosofProfile(sessionToken, model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<List<MainPhotosModel>> GetMainPhotos(string sessionToken)
        {
            try
            {
                return _repository.GetMainphotos(sessionToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteSponsorPhoto(string sessionToken,Guid? Postid,Guid? Userid)
        {
            try
            {
                return _repository.DeleteSponsorPost(sessionToken, Postid, Userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteHomePost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            try
            {
                return _repository.DeleteHomePost(sessionToken, Postid, Userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteSharePost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            try
            {
                return _repository.DeleteSharePost(sessionToken, Postid, Userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public RequestResult<GuidResult> DeleteParticipantsGalleryPost(string sessionToken, Guid? Postid, Guid? Userid)
        {
            try
            {
                return _repository.DeleteParticipantsGalleryPost(sessionToken, Postid, Userid);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
