using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class CH_HomeViewModel
    {
        public Home homeSingledata { get; set; }
        public List<Home> homeList { get; set; }
        public List<Home> Homelistofsubscribed { get; set; }
        public GalleryModel GalleryDataSubscribed { get; set; }
        public List<GalleryModel> GalleryListSubscribed { get; set; }

        public List<ParticipationRegistrationModel> Participantregistration { get; set; }
        public List<OngoingEventparicipantslist> ongoingevent { get; set; }
        public List<VotesModel> voterslist { get; set; }
        public List<CommentModel> CommentList { get; set; }
        public CommentModel AddComment { get; set; }
        public GalleryModel GalleryData { get; set; }
        public List<GalleryModel> GalleryList { get; set; }
        public Sponsor1 sponsorSingleData { get; set; }
        public List<Sponsor1> sponsorMultipleData { get; set; }
        public Sponsor1 sponsoredsSingledata2 { get; set; }
        public List<Sponsor1> sponsorListdata { get; set; }
        public LikesModel likesdata { get; set; }
        public List<ShareModel> Sharedata { get; set; }
        public ShareModel SingleShareData { get; set; }
        public List<AboutEvent> aboutEventlist { get; set; }
        public List<AboutParticipants> aboutParticipantslist1 { get; set; }
        public List<ImportantDates> importantDateslist1 { get; set; }
        public List<AwardsAndRewards> awardRewardlist1 { get; set; }
        public EventsImageandVideo Imagevideo1 { get; set; }
        public SubscribersViewmodel subscriberViewModel { get; set; }
        public List<EventsImageandVideo> ImageVideolist { get; set; }


      
    }
    public class Home
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public Nullable<System.Guid> Postid { get; set; }
        public string PostText { get; set; }
        public byte[] Image { get; set; }
        public byte[] Video { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public HttpPostedFileBase Image1 { get; set; }
        public HttpPostedFileBase Video1 { get; set; }
        public string Image2 { get; set; }
        public string UserName { get; set; }
        public string Profilepic { get; set; }

    }
}
