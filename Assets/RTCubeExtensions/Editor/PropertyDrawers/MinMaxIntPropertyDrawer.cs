using System.Reflection;
using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于<see cref="MinMaxInt"/>类的属性绘制器。
	/// </summary>
	[CustomPropertyDrawer(typeof(MinMaxInt))]
	[Version(1, 0, 0)]
	public class MinMaxIntPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var minProp = property.FindPropertyRelative("min");
			var maxProp = property.FindPropertyRelative("max");

			int minValue = minProp.intValue;
			int maxValue = maxProp.intValue;

			float rangeMin = 0;
			float rangeMax = int.MaxValue;
				
			// 从属性中提取范围
			var rangeAttribute = fieldInfo.GetCustomAttribute<MinMaxRangeAttribute>();

			if (rangeAttribute != null)
			{
				(rangeMin, rangeMax) = rangeAttribute.GetRange();
			}

			float minFloatValue = minValue;
			float maxFloatValue = maxValue;
			
			// 使用提取的范围来绘制 MinMaxSlider
			EditorGUI.MinMaxSlider(position, ref minFloatValue, ref maxFloatValue, rangeMin, rangeMax);

			if (GUI.changed)
			{
				minProp.intValue = Mathf.RoundToInt(minFloatValue);
				maxProp.intValue = Mathf.RoundToInt(maxFloatValue);
			}

			EditorGUI.EndProperty();
		}
	}
}
