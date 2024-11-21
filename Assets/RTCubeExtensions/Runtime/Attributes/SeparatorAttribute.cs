﻿// Copyright RTCube (c) https://runtimecube.com/

using RTCube.Extensions.Internal;
using UnityEngine;

namespace RTCube.Extensions
{
	[Version(1, 0, 0)]
	public class SeparatorAttribute : PropertyAttribute
	{
		public int Height { get; }
		
		public Color Color { get; }
		
		public SeparatorAttribute()
		{
			Height = PropertyDrawerData.SeparatorHeight;
			Color = PropertyDrawerData.SeparatorColor;
		}
		
		public SeparatorAttribute(string color)
		{
			Height = PropertyDrawerData.SeparatorHeight;
			Color = 
				color == null || !ColorExtensions.TryParseHex(color, out var rgbColor) 
					? PropertyDrawerData.SeparatorColor 
					: rgbColor;
		}
		
		public SeparatorAttribute(int height)
		{
			Height = height;
			Color = PropertyDrawerData.SeparatorColor;
		}
		
		public SeparatorAttribute(string color, int height)
		{
			Height = height;
			Color = 
				color == null || !ColorExtensions.TryParseHex(color, out var c) 
					? Color.black 
					: c;
		}
	}
}