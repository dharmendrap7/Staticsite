using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class CommentModel
    {
        public System.Guid Id { get; set; }
        public string DestinationUserId { get; set; }
        public string SourceUserId { get; set; }
        public System.Guid PostId { get; set; }
        public string CommentMessage { get; set; }
        public Nullable<System.DateTime> CommentedDate { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }

    }
    public class CommentResponceModel
    {
        public string CommentDate { set; get; }
        public string UserName { set; get; }
        public string CommentId { set; get; }
        public bool result { set; get; }
        public string CommentMsg { set; get; }
    }
}
