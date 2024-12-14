using System.Collections.Generic;
using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 颜色弹出列表的属性绘制器，用于在Unity编辑器中显示颜色选项的下拉菜单。
	/// </summary>
	[CustomPropertyDrawer(typeof(ColorPopupAttribute))]
	[Version(1, 0, 0)]
	internal class ColorPopupPropertyDrawer : PopupListPropertyDrawer<Color>
	{
		private static Dictionary<Color, Texture> colorBlocks = new Dictionary<Color, Texture>();
		
		/// <summary>
		/// 将颜色值转换为<see cref="GUIContent"/>对象，用于在弹出列表中显示颜色值。
		/// </summary>
		/// <param name="value">要转换的颜色值。</param>
		/// <returns>一个<see cref="GUIContent"/>对象，表示颜色值。</returns>
		protected override GUIContent GetContent(Color value) => CreateColoredSquare(value);

		/// <summary>
		/// 根据弹出列表中选定的选项设置序列化属性的颜色值。
		/// </summary>
		/// <param name="property">要设置值的序列化属性。</param>
		/// <param name="value">要设置的颜色值。</param>
		protected override void SetPropertyValue(SerializedProperty property, Color value) 
			=> property.colorValue = value;
		
		/// <summary>
		/// 获取序列化属性的当前颜色值。
		/// </summary>
		/// <param name="property">要获取值的序列化属性。</param>
		/// <returns>序列化属性的当前颜色值。</returns>
		protected override Color GetValue(SerializedProperty property) => property.colorValue;

		/// <summary>
		/// 创建一个填充了指定颜色的<see cref="Texture2D"/>对象。
		/// </summary>
		/// <param name="color">要填充的颜色。</param>
		/// <returns>一个填充了指定颜色的<see cref="Texture2D"/>对象。</returns>
		public static Texture2D CreateColorTexture(Color color)
		{
			int size = 12;
			var texture = new Texture2D(size, size);
			var pixels = new Color[size * size];

			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = color;
			}

			texture.SetPixels(pixels);
			texture.Apply();
			
			return texture;
		}

		/// <summary>
		/// 创建一个包含填充了指定颜色的正方形的<see cref="GUIContent"/>对象。
		/// </summary>
		/// <param name="color">要创建正方形的颜色。</param>
		/// <returns>一个包含填充了指定颜色的正方形的<see cref="GUIContent"/>对象。</returns>
		public static GUIContent CreateColoredSquare(Color color)
		{
			if(!colorBlocks.ContainsKey(color))
			{
				var texture = CreateColorTexture(color);
				colorBlocks[color] = texture;
			}
			
			return new GUIContent(color.ToString(), colorBlocks[color]);
		}
	}
}
