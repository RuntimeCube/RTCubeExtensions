// Copyright RTCube (c) https://runtimecube.com/

using System.Collections.Generic;
using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions.Algorithms
{
	/// <summary>
	/// A response curve with outputs of Vector3.
	/// </summary>
	[Version(1, 0, 0)]
	public class ResponseCurveVector3:ResponseCurveBase<Vector3>
	{
		#region Constructors

		public ResponseCurveVector3(IEnumerable<float> inputSamples, IEnumerable<Vector3> outputSamples) : base(inputSamples, outputSamples)
		{
		}

		#endregion

		#region Protected Methods

		protected override Vector3 Lerp(Vector3 outputSampleMin, Vector3 outputSampleMax, float t)
		{
			return Vector3.Lerp(outputSampleMin, outputSampleMax, t);
		}

		#endregion
	}
}
