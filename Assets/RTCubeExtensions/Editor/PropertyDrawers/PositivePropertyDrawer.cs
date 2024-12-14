using System;
using UnityEditor;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于<see cref="PositiveAttribute"/>的属性绘制器。
	/// </summary>
	/// <seealso cref="ValidationPropertyDrawer"/>
	[Obsolete("Use " + nameof(ValidationPropertyDrawer) + " instead")]
	[CustomPropertyDrawer(typeof(PositiveAttribute), true)]
	public class PositivePropertyDrawer : ValidationPropertyDrawer
	{
	}
}
