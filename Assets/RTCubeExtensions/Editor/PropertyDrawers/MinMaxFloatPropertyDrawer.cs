// Copyright RTCube (c) https://runtimecube.com/

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于<see cref="MinMaxFloat"/>类的属性绘制器。
	/// </summary>
	[CustomPropertyDrawer(typeof(MinMaxFloat))]
	public class MinMaxFloatPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var minProp = property.FindPropertyRelative("min");
			var maxProp = property.FindPropertyRelative("max");

			float minValue = minProp.floatValue;
			float maxValue = maxProp.floatValue;

			float rangeMin = 0f;
			float rangeMax = 1f;
				
			// 从属性中提取范围
			var rangeAttribute = fieldInfo.GetCustomAttribute<MinMaxRangeAttribute>();

			if (rangeAttribute != null)
			{
				(rangeMin, rangeMax) = rangeAttribute.GetRange();
			}
			
			// 使用提取的范围来绘制 MinMaxSlider
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);

			if (GUI.changed)
			{
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}

			EditorGUI.EndProperty();
		}
	}
}
