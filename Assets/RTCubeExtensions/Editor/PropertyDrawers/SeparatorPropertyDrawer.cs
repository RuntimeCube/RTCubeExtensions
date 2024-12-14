// Copyright RTCube (c) https://runtimecube.com/

using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	[CustomPropertyDrawer(typeof(SeparatorAttribute))]
	[Version(1, 0, 0)]
	public class SeparatorDrawer : DecoratorDrawer
	{
		SeparatorAttribute Attribute => (SeparatorAttribute) attribute;
		
		public override float GetHeight()
		{
			return Attribute.Height;
		}

		public override void OnGUI(Rect position)
		{
			EditorGUI.DrawRect(position, Attribute.Color);
		}
	}
}
