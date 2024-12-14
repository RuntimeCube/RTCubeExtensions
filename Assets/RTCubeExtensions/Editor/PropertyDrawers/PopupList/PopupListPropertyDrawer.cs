using System;
using System.Linq;
using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 用于在Unity编辑器中创建自定义弹出列表抽屉的基类。
	/// </summary>
	/// <typeparam name="T">弹出列表中的值的类型。</typeparam>
	[Version(1, 0, 0)]
	public abstract class PopupListPropertyDrawer<T> : PropertyDrawer
	{
		/// <summary>
		/// 获取与属性关联的<see cref="PopupListAttribute"/>。
		/// </summary>
		public PopupListAttribute Attribute => (PopupListAttribute)attribute;

		protected T[] values;
		
		/// <summary>
		/// 在Unity编辑器中渲染属性，如果值可用，则使用弹出列表。
		/// </summary>
		/// <param name="position">属性应在编辑器窗口中绘制的位置。</param>
		/// <param name="property">正在绘制的序列化属性。</param>
		/// <param name="label">属性的标签。</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (values == null)
			{
				GetValues();
			}
			
			if(values == null)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			DrawField(position, property, label);
		}

		/// <summary>
		/// 在编辑器中绘制弹出列表。
		/// </summary>
		/// <param name="position">属性应在编辑器窗口中绘制的位置。</param>
		/// <param name="property">与属性关联的序列化属性。</param>
		/// <param name="label">属性的标签。</param>
		protected void DrawField(Rect position, SerializedProperty property, GUIContent label)
		{
			int selectedIndex = Array.IndexOf(values, GetValue(property));
			
			if(selectedIndex == -1)
			{
				selectedIndex = 0;
			}

			var style = new GUIStyle(EditorStyles.popup);
			style.imagePosition = ImagePosition.ImageLeft;

			Assert.IsNotNull(values);
			selectedIndex = EditorGUI.Popup(
				position, new GUIContent(label.text), selectedIndex, values.Select(GetContent).ToArray(),
				style);

			if (selectedIndex >= 0 && selectedIndex < values.Length)
			{
				SetPropertyValue(property, values[selectedIndex]);
			}
		}

		/// <summary>
		/// 获取给定值的<see cref="GUIContent"/>。
		/// </summary>
		/// <param name="value">要转换为<see cref="GUIContent"/>的值。</param>
		/// <returns>给定值的<see cref="GUIContent"/>。</returns>
		protected abstract GUIContent GetContent(T value);
		
		/// <summary>
		/// 设置序列化属性的值，基于给定值。
		/// </summary>
		/// <param name="property">要设置值的序列化属性。</param>
		/// <param name="value">要设置的值。</param>
		/// <example>
		/// 如果<typeparamref name="T"/>是<see cref="string"/>，则实现为
		/// <code>
		/// <![CDATA[
		/// 重写受保护的方法 SetPropertyValue(SerializedProperty 属性, string 值) => 属性.stringValue = 值;
		/// ]]>
		/// </code>
		/// </example>
		protected abstract void SetPropertyValue(SerializedProperty property, T value);
		
		/// <summary>
		/// 从序列化属性获取当前值。
		/// </summary>
		/// <param name="property">要获取值的序列化属性。</param>
		/// <returns>序列化属性的当前值。</returns>
		/// <example>
		/// 如果<typeparamref name="T"/>是<see cref="string"/>，则实现为
		/// <code>
		/// <![CDATA[
		/// 重写受保护的字符串获取值（序列化属性属性）=> 属性字符串值；
		/// ]]>
		/// </code>
		/// </example>
		protected abstract T GetValue(SerializedProperty property);

		/// <summary>
		/// 通过系统提供的方法以外的其他方法获取要显示在弹出列表中的值。
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// <remarks>
		/// 需要为<see cref="PopupListAttribute.RetrievalMethod"/>设置为<see cref="ValuesRetrievalMethod.Other"/>的弹出列表实现此方法。
		/// 
		/// </remarks>
		protected virtual T[] GetValuesByOtherMethod() 
			=> throw new NotImplementedException(ErrorMessages.GetValuesByOtherMethodNotImplemented);

		/// <summary>
		/// 根据属性中指定的检索方法获取要显示在弹出列表中的值。
		/// </summary>
		/// <remarks>
		/// 这些值是通过调用编辑器代码获取的，无法从属性本身获取。
		/// </remarks>
		private void GetValues()
		{
			switch (Attribute.RetrievalMethod)
			{
				case ValuesRetrievalMethod.FuncKey:
					values = PropertyDrawerData.GetValues<T>(Attribute.PopupListData.ValuesRetrieverKey);
					break;
				case ValuesRetrievalMethod.ValueList:
					values = ((PopupListData<T>)Attribute.PopupListData).Values;
					break;
				case ValuesRetrievalMethod.Func:
					values = ((PopupListData<T>)Attribute.PopupListData).ListRetriever().ToArray();
					break;
				case ValuesRetrievalMethod.Other:
					values = GetValuesByOtherMethod();
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
