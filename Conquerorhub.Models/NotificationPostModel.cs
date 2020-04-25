using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
    public class NotificationPostModel
    {
        //public string sponsorImage { get; set; }
        //public string galleryImage { get; set; }
        //public byte[] galleryVideo { get; set; }
        //public string partcipantImage { get; set; }
        //public byte[] paticipantVideo { get; set; }
        //public string homeImage { get; set; }
        //public byte[] homeVideo { get; set; }
        public string Image { get; set; }
        public byte[] Video { get; set; }
        public Guid? PostId { get; set; }
        public string sourceID { get; set; }
        public List<LikesModel> LikeList { get; set; }

    }
}
