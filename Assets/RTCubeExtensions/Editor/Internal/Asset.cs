using RTCube.Extensions.Internal;
using UnityEditor;

namespace RTCube.Extensions.Editor.Internal
{
	/// <summary>
	/// 提供用于处理资产的实用方法。
	/// </summary>
	[Version(1, 0, 0)]
	[Experimental]
	public static class Assets
	{
		/// <summary>
		/// 在资产数据库中查找给定类型的所有资产。
		/// </summary>
		/// <typeparam name="T">要查找的资产类型。</typeparam>
		/// <returns>给定类型的一系列资产。</returns>
		[ReuseCandidate]
		public static T[] FindByType<T>() where T : UnityEngine.Object
		{
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
			var assets = new T[guids.Length];

			for (int i = 0; i < guids.Length; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(guids[i]);
				assets[i] = AssetDatabase.LoadAssetAtPath<T>(path);
			}

			return assets;
		}
	}
}
