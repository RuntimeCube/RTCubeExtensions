﻿// Copyright RTCube (c) https://runtimecube.com/

using System.Collections.Generic;
using System.Linq;
using RTCube.Extensions.Internal;

namespace RTCube.Extensions.Algorithms
{
	/// <summary>
	/// A lightweight implementation of an L-system.
	/// </summary>
	/// <typeparam name="TSymbol">This type must be 
	/// comparable using ==, or you should feed an IEqualityComparer.</typeparam>
	[Version(1, 0, 0)]
	public class LSystem <TSymbol>
	{
		#region Private Fields

		private readonly Dictionary<TSymbol, List<TSymbol>> rewriteRules;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new empty LSystem.
		/// </summary>
		public LSystem()
		{
			rewriteRules = new Dictionary<TSymbol, List<TSymbol>>();
		}

		/// <summary>
		/// Constructs a new empty L-System that will use the given comparer to compare symbols.
		/// </summary>
		/// <param name="comparer">The comparer to use to compare symbols.</param>
		public LSystem(IEqualityComparer<TSymbol> comparer)
		{
			rewriteRules = new Dictionary<TSymbol, List<TSymbol>>(comparer);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a new rewrite rule to the system.
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="replacement"></param>
		public void AddRewriteRule(TSymbol symbol, IEnumerable<TSymbol> replacement)
		{
			replacement.ThrowIfNull("replacement");

			rewriteRules[symbol] = replacement.ToList();
		}

		/// <summary>
		/// Rewrites a string using the rewrite rules.
		/// </summary>
		/// <param name="str">The string to rewrite.</param>
		/// <returns>The rewritten string.</returns>
		public IEnumerable<TSymbol> Rewrite(IEnumerable<TSymbol> str)
		{
			str.ThrowIfNull("str");

			return str.SelectMany(Rewrite);
		}

		/// <summary>
		/// Performs a rewrite on a string using the rewrite rules n times.
		/// </summary>
		/// <param name="str">The string to rewrite.</param>
		/// <param name="n">The number of times to rewrite it.</param>
		/// <returns>The rewritten string.</returns>
		public IEnumerable<TSymbol> Rewrite(IEnumerable<TSymbol> str, int n)
		{
			str.ThrowIfNull("str");
			n.ThrowIfNegative("n");

			return n == 0 ? str.ToList() : Rewrite(str, n - 1).SelectMany(Rewrite);
		}

		#endregion

		#region Private Methods

		private IEnumerable<TSymbol> Rewrite(TSymbol symbol)
		{
			if (rewriteRules.TryGetValue(symbol, out var rule))
			{
				foreach (var newSymbol in rule)
				{
					yield return newSymbol;
				}
			}
			else
			{
				yield return symbol;
			}
		}

		#endregion
	}
}
