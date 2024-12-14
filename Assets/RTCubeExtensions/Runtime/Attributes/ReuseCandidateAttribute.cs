using System;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	[Version(1, 0, 0)]
	[Experimental]
	[AttributeUsage(AttributeTargets.All)]
	class ReuseCandidateAttribute : Attribute
	{
		public string MoveToWhere { get; set; }
		
		public string Note { get; set; }
	}
}
