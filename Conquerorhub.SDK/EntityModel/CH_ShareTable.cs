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
    
    public partial class CH_ShareTable
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> PostId { get; set; }
        public Nullable<System.Guid> DestPostid { get; set; }
        public string SourcePage { get; set; }
        public string DestinationPage { get; set; }
        public Nullable<int> ShareCount { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string ContentType { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}