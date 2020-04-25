using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conquerorhub.Models
{
   public class MainPhotosModel
    {

        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public Nullable<System.Guid> PhotosId { get; set; }
        public byte[] BannerPicData { get; set; }
        public byte[] ProfilePicData { get; set; }
        public HttpPostedFileBase BannerPic { get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
    }
}
