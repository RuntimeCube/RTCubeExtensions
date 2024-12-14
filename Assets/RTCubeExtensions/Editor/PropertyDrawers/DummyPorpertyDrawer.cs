// Copyright RTCube (c) https://runtimecube.com/

using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于标记有<see cref="DummyAttribute"/>的属性的属性绘制器。
	/// </summary>
	[CustomPropertyDrawer(typeof(DummyAttribute))]
	public class DummyPorpertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position,
			SerializedProperty prop,
			GUIContent label)
		{
			//Do nothing
		}

		public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
		{
			return 0;
		}
	}
}
