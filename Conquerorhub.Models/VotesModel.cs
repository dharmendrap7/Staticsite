using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
   public class VotesModel
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> PostId { get; set; }
        public string userid { get; set; }
        public Nullable<bool> VoteStatus { get; set; }
        public Nullable<System.Guid> EventId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    }
}
