using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class Sponsors
    {
        public Sponsor1 sponsorSingleData { get; set; }
        public List<Sponsor1> sponsorMultipleData { get; set; } 
        public LikesModel likesdata { get; set; }
        public ShareModel Sharedata { get; set; }

    }
    public class Sponsor1
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }
        public Nullable<int> Like { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> Private { get; set; }
        public Nullable<bool> Public { get; set; }
        public Nullable<System.Guid> ImageId { get; set; }
        public List<string> Images { get; set; }
        public Nullable<System.DateTime> DateandTime { get; set; }
        public List<LikesModel> LikeList { get; set; }
        public List<CommentModel> commentList { get; set; }


        public string Image1 { get; set; }
        public HttpPostedFileBase UploadedImage { get; set; }
        public string Destinationid { get; set; }
        public string UserName { get; set; }
        public string profilepic { get; set; }
        public List<ShareModel> sharelist { get; set; }

    }
}
