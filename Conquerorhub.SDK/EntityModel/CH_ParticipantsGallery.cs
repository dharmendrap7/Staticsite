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
    
    public partial class CH_ParticipantsGallery
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public Nullable<System.Guid> PostId { get; set; }
        public byte[] ImageData { get; set; }
        public string Caption { get; set; }
        public byte[] VideoData { get; set; }
        public Nullable<System.DateTime> DateAndTime { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
