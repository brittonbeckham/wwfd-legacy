//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wwfd.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuoteFlag
    {
        public int FlagID { get; set; }
        public int QuoteID { get; set; }
        public System.Guid FlaggedByContributorID { get; set; }
        public Nullable<System.Guid> FlagRemovedByContributorID { get; set; }
        public string QuoteFlagTypeID { get; set; }
        public System.DateTime DateFlagged { get; set; }
        public Nullable<System.DateTime> DateFlaggedRemoved { get; set; }
        public string Notes { get; set; }
    
        public virtual Contributor Contributor { get; set; }
        public virtual QuoteFlagType QuoteFlagType { get; set; }
        public virtual Quote Quote { get; set; }
    }
}
