// Copyright RTCube (c) https://runtimecube.com/

using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于标记有<see cref="InspectorFlagsAttribute"/>的属性的属性绘制器。
	/// </summary>
	[Version(1, 0, 0)]
	[CustomPropertyDrawer(typeof(InspectorFlagsAttribute))]
	public class InspectorFlagsPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position,
			SerializedProperty prop,
			GUIContent label)
		{
			EditorGUI.showMixedValue = prop.hasMultipleDifferentValues;
			EditorGUI.BeginChangeCheck();

			var newValue = EditorGUI.MaskField(position, label, prop.intValue, prop.enumNames);

			if (EditorGUI.EndChangeCheck())
			{
				prop.intValue = newValue;
			}
		}
	}
}
