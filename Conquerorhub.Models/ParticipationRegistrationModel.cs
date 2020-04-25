using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
    public class ParticipantsDetailModel
    {
        public List<ParticipationRegistrationModel> Participantregistration { get; set; }
        public List<OngoingEventparicipantslist> ongoingevent { get; set; }
        public List<VotesModel> voterslist { get; set; }
        public List<CommentModel> CommentList { get; set; }
        public CommentModel AddComment { get; set; }
        public AboutEvent aboutEvent { get; set; }
        public AboutParticipants aboutParticipants { get; set; }
        public ImportantDates importantDates { get; set; }
        public AwardsAndRewards awardReward { get; set; }
        public tabopen openTab { get; set; }
        public List<AboutEvent> aboutEventlist { get; set; }
        public List<AboutParticipants> aboutParticipantslist { get; set; }
        public List<ImportantDates> importantDateslist { get; set; }
        public List<AwardsAndRewards> awardRewardlist { get; set; }

    }
    public class ParticipationRegistrationModel
    {

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> EventId { get; set; }
        public string OrganizerId { get; set; }
        public string ParticipantId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "College/School name is required")]
        public string CollegeorSchool { get; set; }
        [Required(ErrorMessage = "Qualification is required")]
        public string Qualification { get; set; }
       
        public Nullable<System.DateTime> DateOfBirthh { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        public Nullable<long> ContactNumber { get; set; }
        [Required(ErrorMessage = "About self is required")]
        public string About_Self { get; set; }
        public modeofcompetation Modeofcompitetion { get; set; }
        public Guid Result { get; set; }
        public Registrationstatus RegistrationStatus { get; set; }
        public Nullable<int> OTP { get; set; }
        public Nullable<int> ParticipantStatus { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public Nullable<System.Guid> VideoId { get; set; }
        public Nullable<int> EventStatus { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
        public string Images { get; set; }
     public string UserName { get; set; }
        public string Profilepic { get; set; }
        public Nullable<System.DateTime> DateandTime { get; set; }
    }


    public enum modeofcompetation
    {
        online = 1,
        offline = 2
    }

    public enum Registrationstatus
    {
        Registered = 1,
        
        Particpated = 4,
        NotParticipated = 5

    }
}
