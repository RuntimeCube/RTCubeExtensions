// Copyright RTCube (c) https://runtimecube.com/

using System.Collections.Generic;
using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions.Algorithms
{
	/// <summary>
	/// A response curve with outputs of Vector4.
	/// </summary>
	[Version(1, 0, 0)]
	public class ResponseCurveVector4 : ResponseCurveBase<Vector4>
	{
		#region Constructors

		public ResponseCurveVector4(IEnumerable<float> inputSamples, IEnumerable<Vector4> outputSamples)
			: base(inputSamples, outputSamples)
		{
		}

		#endregion

		#region Protected Methods

		protected override Vector4 Lerp(Vector4 outputSampleMin, Vector4 outputSampleMax, float t)
		{
			return Vector4.Lerp(outputSampleMin, outputSampleMax, t);
		}

		#endregion
	}
}
