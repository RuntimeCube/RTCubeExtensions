﻿using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// Marks a string field that should not be empty.
	/// </summary>
	[Version(1, 0, 0)]
	public class ValidateNotEmptyAttribute : ValidationAttribute
	{
		public ValidateNotEmptyAttribute()
		{
			Message = "Value cannot be empty.";
		}

#if UNITY_EDITOR
		/// <inheritdoc/>
		[EditorOnly]
		public override bool IsValid(UnityEditor.SerializedProperty property)
		{
			switch (property.propertyType)
			{
				case UnityEditor.SerializedPropertyType.String:
					return !string.IsNullOrEmpty(property.stringValue);
				default:
					return true;
			}
		}
#endif
	}
}