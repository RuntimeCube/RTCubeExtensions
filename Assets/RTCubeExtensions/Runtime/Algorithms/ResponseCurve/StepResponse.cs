﻿// Copyright RTCube (c) https://runtimecube.com/

using System.Collections.Generic;

namespace RTCube.Extensions.Algorithms
{

	/// <summary>
	/// A response curve with a step response.
	/// </summary>
	/// <seealso cref="RTCube.Extensions.Algorithms.ResponseCurveBase{Single}" />
	public class StepResponse 
	{
		/// <summary>
		/// Used to indicate whether steps are to the left, middle or right of samples.
		/// </summary>
		public enum StepType
		{
			Left,
			Mid, 
			Right
		}

		/// <summary>
		/// Gets the step response that returns y0 for all inputs less than x, and y1 for 
		/// all inputs greater than or equal to x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y0">The y0.</param>
		/// <param name="y1">The y1.</param>
		/// <returns>StepResponse.</returns>
		public static StepResponse<T> GetStep<T>(float x, T y0, T y1)
		{
			var input = new List<float> { x - 1, x};
			var output = new List<T> { y0, y1 };

			return new StepResponse<T>(input, output, StepType.Right);
		}
	}

	/// <summary>
	/// A response curve with a step response.
	/// </summary>
	/// <seealso cref="RTCube.Extensions.Algorithms.ResponseCurveBase{Single}" />
	public class StepResponse<T> : ResponseCurveBase<T>
	{
		/// <summary>
		/// Gets the step response that returns y0 for all inputs less than x, and y1 for 
		/// all inputs greater than or equal to x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y0">The y0.</param>
		/// <param name="y1">The y1.</param>
		/// <returns>StepResponse.</returns>
		public static StepResponse<T> GetStep(float x, T y0, T y1)
		{
			var input = new List<float> { x - 1, x };
			var output = new List<T> { y0, y1 };

			return new StepResponse<T>(input, output, StepResponse.StepType.Right);
		}

		private readonly StepResponse.StepType stepType;

		public StepResponse(IEnumerable<float> inputSamples, IEnumerable<T> outputSamples, StepResponse.StepType stepType = StepResponse.StepType.Left)
			: base(inputSamples, outputSamples)
		{
			this.stepType = stepType;
		}

		protected override T Lerp(T outputSampleMin, T outputSampleMax, float t)
		{
			switch (stepType)
			{
				default:
				case StepResponse.StepType.Left:
					return outputSampleMin;
				case StepResponse.StepType.Right:
					return outputSampleMax;
				case StepResponse.StepType.Mid:
					return (t < 0.5f) ? outputSampleMin : outputSampleMax;
			}
		}
	}
}
