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
    
    public partial class QuoteTaskHistory
    {
        public int QuoteTaskHistoryID { get; set; }
        public int QuoteTaskTypeID { get; set; }
        public int QuoteID { get; set; }
        public System.Guid ContributorID { get; set; }
        public System.DateTime DateCompleted { get; set; }
    
        public virtual Quote Quote { get; set; }
        public virtual QuoteTaskType QuoteTaskType { get; set; }
    }
}
