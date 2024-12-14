// Copyright RTCube (c) https://runtimecube.com/

using UnityEditor;

using RTCube.Extensions.Editor.Internal;

namespace RTCube.Extensions.Editor
{
	/// <summary>
	/// 这个通用的编辑器使得可以在检查器中添加按钮，这些按钮将执行静态方法。
	/// 通过将<see cref="InspectorButtonAttribute"/>添加到方法中来实现。
	/// </summary>
	/// <seealso cref="RTCube.Extensions.Editor.Internal.RTCEditor{RTCMonoBehaviour}" />
	/// <remarks>你也可以通过扩展RTCEditor并调用DrawInspectorButtons来将此行为添加到你自己的编辑器中。</remarks>
#if !ODIN_INSPECTOR
	[CustomEditor(typeof(RTCMonoBehaviour), true)]
#endif
	[CanEditMultipleObjects]
	public class RTCMonoBehaviourEditor : RTCEditor<RTCMonoBehaviour>
	{
		const int ColumnCount = 3;

		/*
		protected override void OnHeaderGUI()
		{
			//base.OnHeaderGUI();
		}
		*/
		
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			DrawInspectorButtons(ColumnCount);
		}		
	}
}
