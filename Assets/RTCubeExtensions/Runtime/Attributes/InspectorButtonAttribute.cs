// Copyright RTCube (c) https://runtimecube.com/

using System;
using JetBrains.Annotations;

namespace RTCube.Extensions
{
	/// <summary>
	/// <see cref="T:RTCube.Extensions.Editor.Internal.RTCEditor`1.DrawInspectorButtons"/> draws a button for
	/// each method marked with this attribute. This is also used by 
	/// <see cref="T:RTCube.Extensions.Editor.GLMonoBehaviourEditor"/>.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Method), MeansImplicitUse]
	public class InspectorButtonAttribute : Attribute
	{
		// TODO: Add support for custom button names
	}
}
