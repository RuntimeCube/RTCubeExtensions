using System.Collections.Generic;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions.Algorithms
{
	[Version(1, 0, 0)]
	public static class ComparerExtensions
	{
		public static bool Less<T>(this IComparer<T> comparer, T a, T b) => comparer.Compare(a, b) < 0;
	}
}
