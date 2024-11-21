// Copyright RTCube (c) https://runtimecube.com/

using System;

namespace RTCube.Extensions.Internal
{
	/// <summary>
	/// An attribute to mark API versions.
	/// </summary>
	[Version(1, 0, 0)]
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public class VersionAttribute : Attribute
	{
		#region Properties

		/// <summary>
		/// The main version number of this element.
		/// </summary>
		public int MainVersion { get; private set; }

		/// <summary>
		/// The sub version number of this element.
		/// </summary>
		public int SubVersion { get; private set; }

		/// <summary>
		/// The sub-sub version of this element.
		/// </summary>
		public int SubSubVersion { get; private set; }

		#endregion

		#region Constructors

		public VersionAttribute(int mainVersion, int subVersion, int subSubVersion)
		{
			MainVersion = mainVersion;
			SubVersion = subVersion;
			SubSubVersion = subSubVersion;
		}

		#endregion
	}
}
