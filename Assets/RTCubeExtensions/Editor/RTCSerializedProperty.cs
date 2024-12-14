// Copyright RTCube (c) https://runtimecube.com/

using RTCube.Extensions.Internal;
using UnityEditor;
using Object = UnityEngine.Object;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 包装一个SerializedProperty，并提供额外的功能，如工具提示和更强大的Find方法。
	/// </summary>
	[Version(1, 0, 0)]
	public class RTCSerializedProperty
	{
		public SerializedProperty SerializedProperty { get; set; }

		public string CustomTooltip { get; set; }

		public SerializedPropertyType PropertyType => SerializedProperty.propertyType;

		public Object ObjectReferenceValue
		{
			get => SerializedProperty.objectReferenceValue;
			set => SerializedProperty.objectReferenceValue = value;
		}

		
		public int EnumValueIndex
		{
			get => SerializedProperty.enumValueIndex;
			set => SerializedProperty.enumValueIndex = value;
		}

		public string[] EnumNames => SerializedProperty.enumNames;

		public bool BoolValue
		{
			get => SerializedProperty.boolValue;
			set => SerializedProperty.boolValue = value;
		}

		public int IntValue
		{
			get => SerializedProperty.intValue;
			set => SerializedProperty.intValue = value;
		}

		public float FloatValue
		{
			get => SerializedProperty.floatValue;
			set => SerializedProperty.floatValue = value;
		}

		public string StringValue
		{
			get => SerializedProperty.stringValue;
			set => SerializedProperty.stringValue = value;
		}

		public RTCSerializedProperty FindPropertyRelative(string name)
		{
			var property = SerializedProperty.FindPropertyRelative(name);

			return new RTCSerializedProperty
			{
				SerializedProperty = property
			};
		}
	}
}
