// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions
{
	/// <summary>
	/// Used to mark a field to add a comment above the field in the inspector.
	/// </summary>
	/// <seealso cref="UnityEngine.PropertyAttribute" />
	/// <customList>PropertyDrawer</customList>
	[AttributeUsage(AttributeTargets.Field)]
	[Version(1, 0, 0)]
	public class CommentAttribute : PropertyAttribute
	{
		public readonly GUIContent content;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommentAttribute"/> class.
		/// </summary>
		/// <param name="comment">The comment to display in the inspector.</param>
		/// <param name="tooltip">An optional tooltip to display when the user hovers over the comment.</param>
		public CommentAttribute(string comment, string tooltip = "")
		{
			content = string.IsNullOrEmpty(tooltip) ? new GUIContent(comment) : new GUIContent(comment + " [?]", tooltip);
		}
	}
}
