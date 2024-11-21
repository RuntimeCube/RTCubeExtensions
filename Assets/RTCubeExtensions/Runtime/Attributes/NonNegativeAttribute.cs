// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// Mark numeric values that should always be non-negative.
	/// </summary>
	[Version(1, 0, 0)]
	[Obsolete("Use ValidateNonNegativeAttribute instead.")]
	public class NonNegativeAttribute : ValidateNotNegativeAttribute
	{
	}
}
