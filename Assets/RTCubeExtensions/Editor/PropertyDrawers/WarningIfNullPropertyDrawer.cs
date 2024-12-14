using System;
using UnityEditor;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 一个用于标记有<see cref="WarningIfNullAttribute"/>的属性的属性绘制器。
	/// </summary>
	/// <seealso cref="ValidationPropertyDrawer"/>
	[Obsolete("Use " + nameof(ValidationPropertyDrawer) + " instead")]
	[CustomPropertyDrawer(typeof(WarningIfNullAttribute), true)]
	public class WarningIfNullPropertyDrawer : ValidationPropertyDrawer
	{
	}
}
