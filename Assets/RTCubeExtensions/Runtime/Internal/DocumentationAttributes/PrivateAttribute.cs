// Copyright RTCube (c) https://runtimecube.com/

using System;

namespace RTCube.Extensions.Internal
{
	/// <summary>
	/// Use to mark targets that are private, but cannot be implemented as such
	/// because Unity it needs to be public to work with Unity.
	/// </summary>
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class PrivateAttribute : Attribute
	{
	}
}
