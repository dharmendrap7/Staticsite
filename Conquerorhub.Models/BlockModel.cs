using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{

    public class BlockViewModel
    {
        public List<BlockModel> blockModelList { get; set; }
        public List<string> BlockedUserName { get; set; }
        public List<string> ProfilePic { get; set; }
        public  List<string> usertype { get; set; }
    }
   public class BlockModel
    {

        public int Id { get; set; }
        public string BlockingUser { get; set; }
        public string BlockedUser { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
       
    }
}
