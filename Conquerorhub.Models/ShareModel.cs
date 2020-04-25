using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class ShareModel
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> PostId { get; set; }
        public string SourcePage { get; set; }
        public string DestinationPage { get; set; }
        public Nullable<int> ShareCount { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string UserName { get; set; }
        public string ContentType { get; set; }
        public string profilepic { get; set; }
    }
}
