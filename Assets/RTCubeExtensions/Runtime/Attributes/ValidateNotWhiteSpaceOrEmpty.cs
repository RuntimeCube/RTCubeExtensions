using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// Marks a string field that should not be empty or whitespace.
	/// </summary>
	[Version(1, 0, 0)]
	public class ValidateNotWhiteSpaceOrEmpty : ValidateMatchRegularExpressionAttribute
	{
		public ValidateNotWhiteSpaceOrEmpty() : base(@"\S")
		{
			Message = "Value cannot be empty or whitespace.";
		}
	}
}
