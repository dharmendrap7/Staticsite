using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Models
{
    public class SubscribersViewmodel
    {
        public List<SubscribersModel> Subcsriberslist { get; set; }
        public SubscribersModel Subscribers { get; set; }
    }
   public class SubscribersModel
    {
        public System.Guid Id { get; set; }
        public string SubscriberUserId { get; set; }
        public string ProfileUserId { get; set; }
        public Nullable<bool> SubscriptionStatus { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public string Subscribersname { get; set; }
        public string SubscribetoUsename { get; set; }
        public string usertype { get; set; }
        public string Subscribedtoname { get; set; }
        public string profilepic { get; set; }
        public string profilepicSubscribedto { get; set; }
    }
}
