// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions
{
	/// <summary>
	/// Used to mark inspectable fields as read-only (that is, making them uneditable, even if they are visible).
	/// </summary>
	/// <seealso cref="UnityEngine.PropertyAttribute" />
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.Field)]
	public class ReadOnlyAttribute : PropertyAttribute
	{
	}
}
