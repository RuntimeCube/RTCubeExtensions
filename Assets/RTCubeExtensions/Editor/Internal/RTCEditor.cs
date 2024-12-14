// Copyright RTCube (c) https://runtimecube.com/

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace RTCube.Extensions.Editor.Internal
{
	/// <summary>
	/// 可以作为自定义编辑器的基类，提供额外便利方法和属性。
	/// </summary>
	/// <typeparam name="T">这是编辑器所针对的类型。</typeparam>
	/// <seealso cref="UnityEditor.Editor" />
	[Version(1, 0, 0)]
	public class RTCEditor<T> : UnityEditor.Editor
		where T : MonoBehaviour
	{
		public T Target => (T)target;

		public T[] Targets => targets.Cast<T>().ToArray();

		/// <summary>
		/// 在检查器中绘制一条线作为分隔符。
		/// </summary>
		public void Splitter() => RTCEditorGUI.Splitter();


		public static int AddCombo(string[] options, int selectedIndex) => EditorGUILayout.Popup(selectedIndex, options);

		public bool HasProperty(string propertyName)
		{
			var property = serializedObject.FindProperty(propertyName);

			return property != null;
		}

		public RTCSerializedProperty FindProperty(string propertyName)
		{
			var property = new RTCSerializedProperty
			{
				SerializedProperty = serializedObject.FindProperty(propertyName),
				CustomTooltip = string.Empty
			};

			if (property.SerializedProperty == null)
			{
				Debug.LogError("Could not find property " + propertyName + " in class " + typeof(T).Name);

				return property;
			}

			Type type = typeof(T);

			FieldInfo field = type.GetField(propertyName,
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			if (field == null)
			{
				Debug.LogError("Could not find field " + propertyName + " in class " + typeof(T).Name);

				return property;
			}

			return property;
		}

		protected void AddField(SerializedProperty prop)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(prop, true);
			EditorGUILayout.EndHorizontal();
		}

		protected void AddField(RTCSerializedProperty prop)
		{
			if (prop == null) return;
			if (prop.SerializedProperty == null) return;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(prop.SerializedProperty,
				new GUIContent(prop.SerializedProperty.name.SplitCamelCase(), prop.CustomTooltip), true);
			EditorGUILayout.EndHorizontal();
		}

		protected void AddLabel(string title, string text)
		{
			EditorGUILayout.LabelField(title, text);
		}

		protected void AddTextAndButton(string text, string buttonLabel, Action buttonAction)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(text, EditorStyles.boldLabel);

			if (GUILayout.Button(buttonLabel))
			{
				if (buttonAction != null)
					buttonAction();
			}

			EditorGUILayout.EndHorizontal();
		}

		protected void ArrayGUI(SerializedObject obj, SerializedProperty property)
		{
			int size = property.arraySize;

			int newSize = EditorGUILayout.IntField(property.name + " Size", size);

			if (newSize != size)
			{
				property.arraySize = newSize;
			}

			EditorGUI.indentLevel = 3;

			for (int i = 0; i < newSize; i++)
			{
				var prop = property.GetArrayElementAtIndex(i);
				EditorGUILayout.PropertyField(prop);
			}
		}

		/// <summary>
		/// 在检查器中为目标类中的所有标记有InspectorButtonAttribute的方法绘制按钮。
		/// </summary>
		/// <param name="columnCount">要绘制按钮的列数。</param>
		protected void DrawInspectorButtons(int columnCount)
		{
			var methods = GetUniqueMethodsWithAttribute(Target.GetType(), typeof(InspectorButtonAttribute));
			EditorGUILayout.BeginHorizontal();

			for (int i = 0; i < methods.Length; i++)
			{
				var method = methods[i];

				if (GUILayout.Button(method.Name.SplitCamelCase()))
				{
					if (method.ReturnType == typeof(IEnumerator))
					{
						Target.StartCoroutine((IEnumerator)method.Invoke(Target, new object[] { }));
					}
					else
					{
						method.Invoke(Target, new object[] { });
					}
				}

				if (i % columnCount == columnCount - 1)
				{
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
				}
			}

			EditorGUILayout.EndHorizontal();
		}

		private static IEnumerable<Type> GetParentTypes(Type type)
		{
			var currentBaseType = type;

			while (currentBaseType != null)
			{
				yield return currentBaseType;
				currentBaseType = currentBaseType.BaseType;
			}
		}

		private static MethodInfo[] GetUniqueMethodsWithAttribute(Type targetType, Type attributeType)
		{
			var methodsWithAttribute = new Dictionary<string, MethodInfo>();

			foreach (var type in GetParentTypes(targetType))
			{
				var methods = type.GetMethods(
						BindingFlags.NonPublic
						| BindingFlags.Public
						| BindingFlags.Instance
						| BindingFlags.Static)
					.Where(m => m.GetCustomAttributes(attributeType, false).Length > 0);

				foreach (var method in methods)
				{
					string methodSignature = GetMethodSignature(method);

					// 如果方法签名已经在字典中，则表示更派生类已经定义了此方法
					if (!methodsWithAttribute.ContainsKey(methodSignature))
					{
						methodsWithAttribute[methodSignature] = method;
					}
				}
			}

			return methodsWithAttribute.Values.ToArray();
		}

		private static string GetMethodSignature(MethodInfo method)
		{
			var parameters = method.GetParameters();
			string parameterTypes = string.Join(",", parameters.Select(p => p.ParameterType.FullName));
			return $"{method.Name}({parameterTypes})";
		}
	}
}
