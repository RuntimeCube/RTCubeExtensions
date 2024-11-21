// Copyright RTCube (c) https://runtimecube.com/

using System;

namespace RTCube.Extensions.Internal
{
	/// <summary>
	/// Use to mark targets that are only exposed because communication 
	/// between classes is necessary to implement certain Unity features.
	/// Typically, when editor classes need private access to the classes
	/// they edit.
	/// </summary>
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class FriendAttribute : Attribute
	{
	}
}
