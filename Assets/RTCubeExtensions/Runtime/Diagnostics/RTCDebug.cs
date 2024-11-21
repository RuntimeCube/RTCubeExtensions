// Copyright RTCube (c) https://runtimecube.com/

using System.Diagnostics;
using RTCube.Extensions.Internal;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace RTCube.Extensions
{
	/// <summary>
	/// Class that contains methods useful for debugging.
	/// All methods are only compiled if the DEBUG symbol is defined.
	/// </summary>
	public static class RTCDebug
	{
		#region Static Methods

		/// <summary>
		/// Check whether the condition is true, and print an error message if it is not.
		/// </summary>
		[Version(1, 0, 0)]
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, Object context=null)
		{
			if (!condition)
			{
				LogError("Assert failed", message, context);
			}
		}

		[Conditional("DEBUG")]
		public static void Log(object message, Object context = null)
		{
			Debug.Log(message, context);
		}

		[Conditional("DEBUG")]
		public static void LogWarning(object message, Object context = null)
		{
			Debug.LogWarning(message, context);
		}

		[Conditional("DEBUG")]
		public static void LogError(object message, Object context = null)
		{
			Debug.LogError(message, context);
		}

		[Conditional("DEBUG")]
		public static void Log(string type, object message, Object context = null)
		{
			Debug.Log(type + ": " + message, context);
		}

		[Conditional("DEBUG")]
		public static void LogWarning(string type, object message, Object context = null)
		{
			Debug.LogWarning(type + ": " + message, context);
		}

		[Conditional("DEBUG")]
		public static void LogError(string type, object message, Object context = null)
		{
			Debug.LogError(type + ": " + message, context);
		}

		#endregion
	}
}
