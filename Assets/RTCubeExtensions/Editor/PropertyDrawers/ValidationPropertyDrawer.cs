using RTCube.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 属性抽屉，用于派生自<see cref="ValidationAttribute"/>的属性。
	/// </summary>
	/// <remarks>
	/// 这是支持在Unity编辑器中验证和约束值的关键类之一。
	/// 
	/// 请参阅[属性绘制器](../content/PropertyDrawers.md)了解更多详细信息。
	/// 
	/// </remarks>
	[CustomPropertyDrawer(typeof(ValidationAttribute), true)]
	[Version(1, 0, 0)]
	public class ValidationPropertyDrawer : PropertyDrawer
	{
		private ValidationAttribute Attribute => (ValidationAttribute)attribute;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!ShouldDrawWarning(property))
			{
				return base.GetPropertyHeight(property, label);
			}

			var guiContent = new GUIContent(Attribute.Message);
			bool oldWordWrap = EditorStyles.miniLabel.wordWrap;

			EditorStyles.miniLabel.wordWrap = true;

			float height =
				EditorStyles.miniLabel.CalcHeight(guiContent, Screen.width - 19) +
				EditorGUI.GetPropertyHeight(property, label, true);
			EditorStyles.miniLabel.wordWrap = oldWordWrap;

			return height;

		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!ShouldDrawWarning(property))
			{
				DrawAndCheckField(position, property);
				
				return;
			}

			var guiContent = new GUIContent(Attribute.Message);
			bool oldWordWrap = EditorStyles.miniLabel.wordWrap;
			EditorStyles.miniLabel.wordWrap = true;

			var color = GUI.color;
			var contentColor = GUI.contentColor;
			var backgroundColor = GUI.backgroundColor;

			if (EditorGUIUtility.isProSkin)
			{			
				GUI.color = Attribute.Color;
			}
			else
			{
				EditorGUI.DrawRect(position, Attribute.Color);
				GUI.contentColor = Color.black;
				GUI.backgroundColor = Attribute.Color;
			}

			float graphHeight = EditorGUI.GetPropertyHeight(property, label, true);
			float labelHeight = EditorStyles.miniLabel.CalcHeight(guiContent, Screen.width - 19);
			position.height = labelHeight;
			EditorGUI.LabelField(position, Attribute.Message, EditorStyles.miniLabel);
					
			position.y += labelHeight;
			position.height = graphHeight;

			EditorGUI.PropertyField(position, property);
			EditorStyles.miniLabel.wordWrap = oldWordWrap;

			if (EditorGUIUtility.isProSkin)
			{
				GUI.color = color;
			}
			else
			{
				GUI.contentColor = contentColor;
				GUI.backgroundColor = backgroundColor;
			}
		}

		private bool ShouldWarnInConsole(SerializedProperty property) 
			=> Attribute.WarnInConsole && !Attribute.IsValid(property);
		
		private bool ShouldDrawWarning(SerializedProperty property) 
			=> !Attribute.ForceValue && Attribute.WarnInInspector && !Attribute.IsValid(property);

		private void DrawAndCheckField(Rect position, SerializedProperty property)
		{
			EditorGUI.PropertyField(position, property);
			
			if(ShouldForce(property))
			{
				Attribute.ConstrainAndVerify(property);
			}
			else if (ShouldWarnInConsole(property))
			{
				Debug.LogWarning(Attribute.Message);
			}
		}

		private bool ShouldForce(SerializedProperty property) 
			=> Attribute.ForceValue && !Attribute.IsValid(property);
	}
}