using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于字符串弹出列表的属性绘制器，用于在 Unity 编辑器中显示一个包含字符串选项的下拉菜单。
	/// </summary>
	[CustomPropertyDrawer(typeof(StringPopupAttribute))]
	[CustomPropertyDrawer(typeof(TagPopupAttribute))]
	[CustomPropertyDrawer(typeof(LayerPopupAttribute))]
	[Version(1, 0, 0)]
	public class StringPopupPropertyDrawer : PopupListPropertyDrawer<string>
	{
		/// <summary>
		/// 将字符串值转换为用于在弹出列表中显示的<see cref="GUIContent"/>对象。
		/// </summary>
		/// <param name="value">要转换的字符串值。</param>
		/// <returns>一个表示字符串值的<see cref="GUIContent"/>对象。</returns>
		protected override GUIContent GetContent(string value) => new GUIContent(value);

		/// <summary>
		/// 设置序列化属性的字符串值，基于弹出列表中选定的选项。
		/// </summary>
		/// <param name="property">要设置值的序列化属性。</param>
		/// <param name="value">要设置的字符串值。</param>
		protected override void SetPropertyValue(SerializedProperty property, string value) 
			=> property.stringValue = value;
		
		/// <summary>
		/// 获取序列化属性的当前字符串值。
		/// </summary>
		/// <param name="property">要获取值的序列化属性。</param>
		/// <returns>序列化属性的当前字符串值。</returns>
		protected override string GetValue(SerializedProperty property) => property.stringValue;
	}
}
