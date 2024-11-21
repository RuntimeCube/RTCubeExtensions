// Copyright RTCube (c) https://runtimecube.com/

using System.Collections.Generic;
using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions.Algorithms
{
	/// <summary>
	/// A response curve with outputs of Vector2.
	/// </summary>
	[Version(1, 0, 0)]
	public class ResponseCurveVector2 : ResponseCurveBase<Vector2>
	{
		#region Constructors

		public ResponseCurveVector2(IEnumerable<float> inputSamples, IEnumerable<Vector2> outputSamples)
			: base(inputSamples, outputSamples)
		{
		}

		#endregion

		#region Protected Methods

		protected override Vector2 Lerp(Vector2 outputSampleMin, Vector2 outputSampleMax, float t)
		{
			return Vector2.Lerp(outputSampleMin, outputSampleMax, t);
		}

		#endregion
	}
}
