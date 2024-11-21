// Copyright RTCube (c) https://runtimecube.com/

using System;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// Class for representing a bounded range.
	/// </summary>
	[Version(1, 0, 0)]
	[Serializable]
	public class MinMaxInt
	{
		#region Public Fields

		public int min = 0;
		public int max = 1;

		#endregion

		public MinMaxInt()
		{
			min = 0;
			max = 1;
		}

		public MinMaxInt(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
