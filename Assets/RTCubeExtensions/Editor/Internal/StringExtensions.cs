// Copyright RTCube (c) https://runtimecube.com/

using System.Text.RegularExpressions;

namespace RTCube.Extensions.Editor.Internal
{
	/// <summary>
	/// string 扩展方法。
	/// </summary>
	//  From: http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/791963c8-9e20-4e9e-b184-f0e592b943b0/
	public static class StringExtensions
	{
		/// <summary>
		/// 将驼峰命名法转换为分隔单词，并每个单词首字母大写。
		/// </summary>
		/// <param name="str">要转换的字符串。</param>
		/// <returns>转换后的字符串。</returns>
		public static string SplitCamelCase(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}

			string camelCase = Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
			string firstLetter = camelCase.Substring(0, 1).ToUpper();

			if (str.Length > 1)
			{
				string rest = camelCase.Substring(1);

				return firstLetter + rest;
			}
			else
			{
				return firstLetter;
			}
		}
	}
}
