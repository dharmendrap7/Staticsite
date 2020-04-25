using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
  public  class SubCommentModel
    {
        public System.Guid SubCommentId { get; set; }
        public string DestinationUserId { get; set; }
        public string SourceUserId { get; set; }
        public Nullable<System.Guid> PostId { get; set; }
        public System.Guid CommentId { get; set; }
        public Nullable<System.DateTime> SubCommentDatetime { get; set; }
        public string SubCommentmsg { get; set; }
        public AspNetUser AspNetUser { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
    }
  
}
