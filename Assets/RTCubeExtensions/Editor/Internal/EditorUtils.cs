// Copyright RTCube (c) https://runtimecube.com/

using UnityEngine;
using UnityEditor;

namespace RTCube.Extensions.Editor.Internal
{
	/// <summary>
	/// 提供用于编辑器代码的实用方法。
	/// </summary>
	public static class EditorUtils
	{
		/// <summary>
		/// 为颜色数组属性绘制色板。
		/// </summary>
		/// <param name="colorsProp">颜色属性。</param>
		/// <param name="position">坐标。</param>
		public static void DrawColors(SerializedProperty colorsProp, Rect position)
		{
			int colorCount = colorsProp.arraySize;

			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			int columns = (int)(position.width / 16);

			float x = position.x;
			float width = 16;
			float height = 16;
			float y = position.y;

			if (columns > 0)
			{
				var indentLevel = EditorGUI.indentLevel;

				EditorGUI.indentLevel = 0;

				for (int i = 0; i < Mathf.Min(100, colorCount); i++)
				{
					if (i != 0 && i % columns == 0)
					{
						x = position.x;
						y += height;
					}

					var colorProp = colorsProp.GetArrayElementAtIndex(i);
					var color = colorProp.colorValue;

					EditorGUIUtility.DrawColorSwatch(new Rect(x, y, width - 2, height - 2), color);
					x += width;
				}

				EditorGUI.indentLevel = indentLevel;
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// 为颜色列表绘制色板。
		/// </summary>
		/// <param name="colorList">颜色列表。</param>
		/// <param name="position">绘制色板的位置。</param>
		/// <param name="columns">列的数量。</param>
		/// <param name="maxColors">要绘制的最大颜色数。</param>
		/// <param name="widthOffset">宽度偏移量。</param>
		/// <param name="heightOffset">高度偏移量。</param>
		public static void DrawColors(
			Color[] colorList,
			Rect position,
			int columns = 2,
			int maxColors = 100,
			float widthOffset = -2.0f,
			float heightOffset = -2.0f)
		{
			var colorCount = colorList.Length;

			EditorGUILayout.BeginVertical();
			EditorGUILayout.BeginHorizontal();

			var x = position.x;
			var width = position.width / columns;
			var height = position.height;
			var y = position.y;

			if (columns > 0)
			{
				var indentLevel = EditorGUI.indentLevel;

				EditorGUI.indentLevel = 0;

				for (int i = 0; i < Mathf.Min(maxColors, colorCount); i++)
				{
					//Makes a new row
					if (i != 0 && i % columns == 0)
					{
						x = position.x;
						y += height;
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginHorizontal();
					}

					var color = colorList[i];

					EditorGUIUtility.DrawColorSwatch(new Rect(x, y, width + widthOffset, height + heightOffset), color);
					x += width;
				}

				EditorGUI.indentLevel = indentLevel;
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// 从起始矩形到结束矩形绘制一条曲线。
		/// </summary>
		/// <param name="start">起始矩形。</param>
		/// <param name="end">结束矩形。</param>
		public static void DrawNodeCurve(Rect start, Rect end)
		{
			var endPos = new Vector3(end.x, end.y + end.height / 2, 0);

			DrawNodeCurve(start, endPos);
		}

		/// <summary>
		/// 从起始位置到结束位置绘制一条曲线。
		/// </summary>
		/// <param name="start">起始矩形。</param>
		/// <param name="endPosition">结束位置。</param>
		public static void DrawNodeCurve(Rect start, Vector3 endPosition)
		{
			var startPosition = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
			var startTangent = startPosition + Vector3.right * 50;
			var endTangent = endPosition + Vector3.left * 50;
			var shadowColor = new Color(0, 0, 0, 0.06f);

			for (int i = 0; i < 3; i++)
			{
				// 画一个阴影
				Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, shadowColor, null, (i + 1) * 5);
			}

			Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, Color.black, null, 1);

			var oldColor = Handles.color;

			Handles.color = new Color(.5f, 0.1f, 0.1f);

			Handles.DrawSolidDisc(
				(startPosition + endPosition) / 2,
				Vector3.forward,
				5);

			Handles.color = Color.black;

			Handles.DrawWireDisc(
				(startPosition + endPosition) / 2,
				Vector3.forward,
				5);

			Handles.color = oldColor;
		}
	}
}
