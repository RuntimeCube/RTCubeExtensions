// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 类型检查器列表的属性抽屉
	/// </summary>
	[Version(1, 0, 0)]
	[CustomPropertyDrawer(typeof (InspectorList), true)]
	[Obsolete]
	public class InspectorListPropertyDrawer : PropertyDrawer
	{
		private ReorderableList reorderableList;
		private float lastHeight = 0;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var list = property.FindPropertyRelative("values");

			if (list == null)
			{
				return 0;
			}

			InitList(list, property);

			if (reorderableList != null)
			{
				return reorderableList.GetHeight();
			}

			return lastHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var list = property.FindPropertyRelative("values");

			if (list == null)
			{
				return;
			}

			int indentLevel = EditorGUI.indentLevel;

			InitList(list, property);

			if (list.arraySize > 0)
				reorderableList.elementHeight = EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(0));

			if(position.height <= 0)
			{
				return;
			}

			lastHeight = reorderableList.GetHeight();

			reorderableList.DoList(position);
			
			EditorGUI.indentLevel = indentLevel;
		}

		public void InitList(SerializedProperty list, SerializedProperty property)
		{
			if (reorderableList != null)
			{
				return;
			}

			reorderableList = new ReorderableList(property.serializedObject, list, true, true, true, true)
			{
				drawElementCallback = DrawElement,
				drawHeaderCallback = DrawHeader,
#if UNITY_5
					elementHeightCallback =
 index => EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(index), null, true)
#endif

			};
			
			return;

			void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
			{
				var element = list.GetArrayElementAtIndex(index);
				var labelProperty = element;
				var potentialProperty = (SerializedProperty)null;
				int maxCheck = 0;

				while (labelProperty.Next(true) && maxCheck++ < 3)
				{
					if (labelProperty.propertyType == SerializedPropertyType.String)
					{
						if (labelProperty.name == "name" || potentialProperty == null)
						{
							potentialProperty = labelProperty;
							break;
						}
					}
				}

				var itemLabel = potentialProperty == null
					? new GUIContent("Element: " + index)
					: new GUIContent(labelProperty.stringValue);

				EditorGUI.PropertyField(rect, list.GetArrayElementAtIndex(index), itemLabel, true);
			}
			
			void DrawHeader(Rect rect)
			{
				EditorGUI.indentLevel++;
				EditorGUI.LabelField(rect, property.displayName);
			}
		}
	}
}
