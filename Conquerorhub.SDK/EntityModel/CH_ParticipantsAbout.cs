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
    
    public partial class CH_ParticipantsAbout
    {
        public System.Guid Id { get; set; }
        public string Userid { get; set; }
        public string BasicDetails { get; set; }
        public string ContactDetails { get; set; }
        public string EducationDetails { get; set; }
        public Nullable<System.DateTime> DateandTime { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
