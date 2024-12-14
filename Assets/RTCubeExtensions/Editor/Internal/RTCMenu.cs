// Copyright Gamelogic (c) http://www.gamelogic.co.za

using UnityEngine;
using UnityEditor;

namespace RTCube.Extensions.Editor.Internal
{
	/// <summary>
	/// 包含静态函数用于菜单选项。
	/// </summary>
	// ReSharper 禁用一次部分类型具有单个部分（其他部分在其他插件中定义）
	public static partial class RTCMenu
	{
		public static void OpenUrl(string url) => Application.OpenURL(url);

		[MenuItem("Help/RTCube/AssetStore/RuntimeMapMaker 3D")]
		public static void OpenAssetStore()
		{
			OpenUrl("https://assetstore.unity.com/publishers/50211");
		}

		[MenuItem("Help/RTCube/Extensions/API Documentation")]
		public static void OpenExtensionsAPI()
		{
			OpenUrl("https://runtimecube.com/rtcextension-doc/api/RTCube.Extensions.html");
		}
	}
}
