//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Conquerorhub.SDK.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CH_SubscriptionTable
    {
        public System.Guid Id { get; set; }
        public string SubscriberUserId { get; set; }
        public string ProfileUserId { get; set; }
        public Nullable<bool> SubscriptionStatus { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
