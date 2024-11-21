// Copyright RTCube (c) https://runtimecube.com/

using System;

namespace RTCube.Extensions.Internal
{
	/// <summary>
	/// Use to mark classes and methods that are abstract, but cannot be implemented as such
	/// because Unity does not serialize such classes properly, especially abstract ScriptableObjects.
	/// </summary>
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.Class |
	                AttributeTargets.Struct |
	                AttributeTargets.Method |
	                AttributeTargets.Property, Inherited = false)]
	public sealed class AbstractAttribute : Attribute
	{
	}
}
