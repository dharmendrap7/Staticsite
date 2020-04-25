using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
  public  class SessionTokenModel
    {
        public System.Guid id { get; set; }
        public string SessionToken1 { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryDateandTime { get; set; }
        public DateTime CreatedDateandTime { get; set; }
    }
}
