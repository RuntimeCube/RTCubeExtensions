// Copyright RTCube (c) https://runtimecube.com/

using UnityEngine;
using UnityEditor;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于标记有<see cref="CommentAttribute"/>的属性的属性绘制器。类似于Header，但用于更长的描述。
	/// </summary>
	[CustomPropertyDrawer(typeof(CommentAttribute), useForChildren: true)]
	public class CommentPropertyDrawer : DecoratorDrawer
	{
		CommentAttribute CommentAttribute => (CommentAttribute)attribute;

		public override float GetHeight() => EditorStyles.wordWrappedLabel.CalcHeight(CommentAttribute.content, EditorGUIUtility.currentViewWidth - 30);

		public override void OnGUI(Rect position)
		{
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.LabelField(position, CommentAttribute.content, EditorStyles.wordWrappedLabel);
			EditorGUI.EndDisabledGroup();
		}
	}
}
