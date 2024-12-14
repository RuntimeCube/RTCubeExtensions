// Copyright RTCube (c) https://runtimecube.com/

using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于标记有<see cref="HighlightAttribute"/>的属性的属性绘制器。
	/// </summary>
	[Version(1, 0, 0)]
	[CustomPropertyDrawer(typeof(HighlightAttribute))]
	public class HighlightPropertyDrawer : PropertyDrawer
	{
		private HighlightAttribute Attribute => (HighlightAttribute) attribute;
		
		public override void OnGUI(Rect position,
			SerializedProperty property,
			GUIContent label)
		{
			
			var newColor = Attribute.color;
			
			if (EditorGUIUtility.isProSkin)
			{			
				var oldColor = GUI.color;
				GUI.color = newColor;
				EditorGUI.PropertyField(position, property, label);
				GUI.color = oldColor;
			}
			else
			{
				var oldBackgroundColor = GUI.backgroundColor;
				var oldContentColor = GUI.contentColor;
				
				EditorGUI.DrawRect(position, newColor);
				GUI.contentColor = Color.black;
				GUI.backgroundColor = newColor;
				
				EditorGUI.PropertyField(position, property, label);
				GUI.backgroundColor = oldBackgroundColor;
				GUI.contentColor = oldContentColor;
			}
		}
	}
}
