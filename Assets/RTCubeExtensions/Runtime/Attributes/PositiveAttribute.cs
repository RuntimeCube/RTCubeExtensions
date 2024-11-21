// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// Mark fields that should always be positive with this attribute.
	/// </summary>
	[Obsolete("Use ValidatePositiveAttribute instead.")]
	[Version(1, 0, 0)]
	public class PositiveAttribute : ValidatePositiveAttribute
	{
	}
}
