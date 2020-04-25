using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class OngoingEventparicipantslist
    {

        public System.Guid Id { get; set; }
        public Nullable<System.Guid> Eventid { get; set; }
        public string UserId { get; set; }
        public Nullable<int> ParticipantStatus { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public Nullable<int> EventStatus { get; set; }
        public Nullable<System.Guid> VideoId { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
