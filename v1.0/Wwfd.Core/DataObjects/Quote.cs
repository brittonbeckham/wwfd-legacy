using System;
using System.Runtime.Serialization;

namespace Wwfd.Core.DataObjects
{
	[DataContract]
	public class Quote
	{
		[DataMember]
		public int QuoteId { get; set; }
		[DataMember]
		public string QuoteText { get; set; }
		[DataMember]
		public string FounderName { get; set; }
		[DataMember]
		public Uri FounderImageThumbnail { get; set; }
		[DataMember]
		public int FounderId { get; set; }
		[DataMember]
		public string Keywords { get; set; }
		[DataMember]
		public string ReferenceInfo { get; set; }
	}
}