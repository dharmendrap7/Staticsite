using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
  public  class SubTypeEventsModel
    {
        public int Id { get; set; }
        public Nullable<int> EventReferenceId { get; set; }
        public string EventSubType { get; set; }
    }
}
