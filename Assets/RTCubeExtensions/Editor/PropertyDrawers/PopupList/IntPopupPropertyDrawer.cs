using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于整数弹出列表的属性绘制器，用于在 Unity 编辑器中显示一个包含整数选项的下拉菜单。
	/// </summary>
	[CustomPropertyDrawer(typeof(IntPopupAttribute))]
	[Version(1, 0, 0)]
	public class IntPopupPropertyDrawer : PopupListPropertyDrawer<int>
	{
		/// <summary>
		/// 将整数值转换为用于在弹出列表中显示的<see cref="GUIContent"/>对象。
		/// </summary>
		/// <param name="value">要转换的整数值。</param>
		/// <returns>一个表示整数值的<see cref="GUIContent"/>对象。</returns>
		protected override GUIContent GetContent(int value) => new GUIContent(value.ToString());

		/// <summary>
		/// 设置序列化属性的整数值，基于弹出列表中选定的选项。
		/// </summary>
		/// <param name="property">要设置值的序列化属性。</param>
		/// <param name="value">要设置的整数值。</param>
		protected override void SetPropertyValue(SerializedProperty property, int value) => property.intValue = value;
		
		/// <summary>
		/// 获取序列化属性的当前整数值。
		/// </summary>
		/// <param name="property">要获取值的序列化属性。</param>
		/// <returns>序列化属性的当前整数值。</returns>
		protected override int GetValue(SerializedProperty property) => property.intValue;
	}
}