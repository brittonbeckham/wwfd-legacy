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
    
    public partial class GetRandomQuote_Result
    {
        public int QuoteID { get; set; }
        public System.Guid ContributorID { get; set; }
        public int FounderID { get; set; }
        public string QuoteText { get; set; }
        public string ReferenceInfo { get; set; }
        public string Keywords { get; set; }
        public System.DateTime DateAdded { get; set; }
        public bool Approved { get; set; }
        public Nullable<int> QuoteLength { get; set; }
        public string FullName { get; set; }
    }
}
