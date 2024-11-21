using System;

namespace RTCube.Extensions.Internal
{
	/// <summary>
	/// Use to mark targets that are only supposed to be used by internal editor code.
	/// </summary>
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class EditorInternal : Attribute
	{
	}
}
