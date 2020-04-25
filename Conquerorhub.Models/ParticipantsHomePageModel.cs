using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class ParticipantsHomePageModel
    {
        public GalleryModel GalleryData { get; set; }
        public List<GalleryModel> GalleryList { get; set; }
        public LikesModel likesdata { get; set; }
        public List<ShareModel> Sharedata { get; set; }
        public ShareModel SingleShareData { get; set; }

    }
}
