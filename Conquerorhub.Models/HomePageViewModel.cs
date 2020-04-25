using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class HomePageViewModel
    {
        public List<ParticipationRegistrationModel> Participantregistration { get; set; }
        public List<OngoingEventparicipantslist> ongoingevent { get; set; }
        public List<VotesModel> voterslist { get; set; }
        public List<CommentModel> CommentList { get; set; }
        public CommentModel AddComment { get; set; }
        public GalleryModel GalleryData { get; set; }
        public List<GalleryModel> GalleryList { get; set; }
        public Sponsor1 sponsorSingleData { get; set; }
        public List<Sponsor1> sponsorMultipleData { get; set; }
        public LikesModel likesdata { get; set; }
        public List<ShareModel> Sharedata { get; set; }
        public ShareModel SingleShareData { get; set; }
        public List<AboutEvent> aboutEventlist { get; set; }
        public List<AboutParticipants> aboutParticipantslist1 { get; set; }
        public List<ImportantDates> importantDateslist1 { get; set; }
        public List<AwardsAndRewards> awardRewardlist1 { get; set; }
        public EventsImageandVideo Imagevideo1 { get; set; }
        public List<EventsImageandVideo> Imagevideolist { get; set; }
        public List<EventRegistrationfromOrganizerModel> EventRegistration { get; set; }
        public Home homeSingledata { get; set; }
        public List<Home> homeList { get; set; }

    }
}
