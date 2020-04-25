using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class EventRegistrationfromOrganizerModel
    {
       
      public AboutEvent aboutEvent { get; set; }
        public AboutParticipants aboutParticipants { get; set; }
        public ImportantDates importantDates { get; set; }
        public AwardsAndRewards awardReward { get; set; }
        public tabopen openTab { get; set; }

        public List<AboutEvent> aboutEventlist { get; set; }
        public List<AboutParticipants> aboutParticipantslist { get; set; }
        public List<ImportantDates> importantDateslist { get; set; }
        public List<AwardsAndRewards> awardRewardlist { get; set; }
        public EventsImageandVideo Imagevideo { get; set; }
        public List<EventsImageandVideo> ImagevideoList { get; set; }
        


    }



    public class EventDetailsViewModel
    {
        public List<AboutEvent> eventDetails { get;set;}
        public List<string> EventSubType { get; set; }

    }
    public class AboutEvent
    {
        public Guid Result { get; set; }
        public System.Guid? EventId { get; set; }
        public string OrganizerId { get; set; }
        [Required(ErrorMessage ="Name Of organizer is required")]
        public string NameofOrganizer { get; set; }
        [Required(ErrorMessage ="Event name is required")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Mode of event is required")]
        public ModeofEvent modeofevent { get; set; }
        [Required(ErrorMessage ="Type of event is required")]
        public TypeofEvent TypeOfEvent { get; set; }
        [Required(ErrorMessage ="Subtype of event is required")]
        public int SubTypeOfEvent { get; set; }
      
        
        public string SubTypeOfEventstring { get; set; }
        public Nullable<int> EventStatus { get; set; }
        public ExpectingVideoorImage ImageorVideo { get; set; }
        [Required(ErrorMessage = "Entrance fee is required")]
        public Nullable<int> Eventfee { get; set; }
        public Nullable<bool> PaymentStatus { get; set; }
        public string Profilepic { get; set; }
        
       
    }
    public class AboutParticipants
    {
        public Guid Result { get; set; }
        public string OrganizerId { get; set; }
        public System.Guid EventId { get; set; }

        [Required(ErrorMessage = "Stream od participants is required")]
        public streamofparticipants StreamOfParticipants { get; set; }
        [Required(ErrorMessage = "Age group of participants is required")]
        public Agegroup AgeGroup { get; set; }
        [Required(ErrorMessage = "Max number of team accepted is required")]
        public Numberofteam MaxnumofTeam { get; set; }
        [Required(ErrorMessage = "Number of memebersin a team is required")]
        public teamsize numofParticipantsinteam { get; set; }
        public Nullable<int> EventStatus { get; set; }
    }
   
    public enum streamofparticipants
    {
        none=0,
        School =1,
        Preuniversity=2,
        UnderGraduate=3,
              PostGraduate=4
    }
    public enum Agegroup
    {
        
        Less_than_18=1,
        Greaterthan_18_Lessthan_50 = 2,
        SeniorCitizens_greaterthan_50 = 3,
        others = 4

    }
    public enum Numberofteam
    {
       none=0,
       Twenty=20,
       Thirty=30,
       Fifty=50,
       Hundred=100,
       NoLimit=6
       //Custom=7

    }
    public enum ModeofEvent
    {
        Online=1,
        Offline=2,
        both=3
        
    }
    public enum ExpectingVideoorImage
    {
        Image=1,
        Video=2,
        None=3
    }
    public enum TypeofEvent
    {
        Arts=1,
        Business=2,
        Cultural=3,
        Technical=4,
        Awareness=5,
        Workshop=6
    }
    public enum teamsize   // next
     
    {
        none = 0,
        solo =1,
        less_than_5=2,
        Custom_Limit=3
    }

    public class ImportantDates
    {
        public Guid Result { get; set; }
        public string OrganizerId { get; set; }
        public System.Guid EventId { get; set; }
        [Required(ErrorMessage = "Event Registration date is required")]
        public Nullable<System.DateTime> StartofEventRegistration { get; set; }
        [Required(ErrorMessage = "End of event registration date is required")]
        public Nullable<System.DateTime> EndOfEventRegistration { get; set; }
        [Required(ErrorMessage = "Start date for uploading performance is required")]
        public Nullable<System.DateTime> StartofVideoUpload { get; set; }
        [Required(ErrorMessage = "End date for uploading video is required")]
        public Nullable<System.DateTime> EndOfVideoUpload { get; set; }
        [Required(ErrorMessage = "Start date for valuation from voters is required")]
        public Nullable<System.DateTime> StartOfValuationfromvoters { get; set; }
        [Required(ErrorMessage = "End date for valuation from voters is required")]
        public Nullable<System.DateTime> EndofValuationFromVoters { get; set; }
        [Required(ErrorMessage = "Result date is required")]
        public Nullable<System.DateTime> ResultDate { get; set; }
        public Nullable<int> EventStatus { get; set; }
        public Nullable<System.DateTime> CurrentDateandTime { get; set; }
    }
    public class AwardsAndRewards
    {
        public Guid Result { get; set; }
        public string OrganizerId { get; set; }
        public System.Guid EventId { get; set; }
        [Required(ErrorMessage = "First prize is required")]
        public string FirstPrize { get; set; }
        [Required(ErrorMessage = "Second prize is required")]
        public string SecondPrize { get; set; }
        [Required(ErrorMessage = "Third prize is required")]
        public string ThirdPrize { get; set; }
        public Nullable<int> EventStatus { get; set; }
    }
    public class tabopen
    {
        public bool AboutEvent { get; set; }
        public bool AboutParticipant { get; set; }
        public bool Imortantdates { get; set; }
        public bool Awarcreward { get; set; }
        public bool Default { get; set; }
        public bool EventVideoImage { get; set; }
        public bool PaymentGateway { get; set; }
        public Nullable<int> EventStatus { get; set; }
    }
    public class EventsImageandVideo
    {
        public string OrganizerId { get; set; }
        public System.Guid? EventIdImageorVideo { get; set; }
        public string Image { get; set; }
        public byte[] EventImage { get; set; }
        public byte[] EventVideo { get; set; }
       
        public HttpPostedFileBase PostedVideo { get; set; }
        [Required(ErrorMessage = "Event related picture is mandatory")]
        public HttpPostedFileBase PostedImage { get; set; }
        public bool? Homedisplayevent { get; set; }
        public string UserName { get; set; }
       
        public int? imageorvideo { get; set; }
        public Nullable<int> EventStatus { get; set; }
        public int? EventMode { get; set; }
        public string Profilepic { get; set; }
        public DateTime? Datetime { get; set; }
    }
}
