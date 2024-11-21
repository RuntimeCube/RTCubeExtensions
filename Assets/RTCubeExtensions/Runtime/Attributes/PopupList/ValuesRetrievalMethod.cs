using System;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions
{
	/// <summary>
	/// How the values for the popup list should be retrieved.
	/// </summary>
	[Version(1, 0, 0)]
	public enum ValuesRetrievalMethod
	{
		/// <summary>
		/// Retrieved by calling a <see cref="Func{TResult}"/> registered with
		/// <see cref="PropertyDrawerData.RegisterValuesRetriever{T}"/> using a key. 
		/// </summary>
		FuncKey,
		
		/// <summary>
		/// Retrieved directly from a list of values.
		/// </summary>
		ValueList,
		
		/// <summary>
		/// Retrieved calling a <see cref="Func{TResult}"/>.
		/// </summary>
		Func,
		
		/// <summary>
		/// Retrieved using a different method. 
		/// </summary>
		/* Design note: Added to allow users to create attributes that retrieve values in a different way. */
		Other
	}
}
