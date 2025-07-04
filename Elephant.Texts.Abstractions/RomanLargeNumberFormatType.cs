namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// Roman large number format for Roman numeral values > 3999.
	/// </summary>
	public enum RomanLargeNumberFormatType
	{
		/// <summary> Uses M, MM, MMM, etc.</summary>
		MPrefix,

		/// <summary> Uses overlines (V̅ = 5000, X̅ = 10000). Represented with Unicode combining overline. A.k.a. Vinculum.</summary>
		Overline,

		/// <summary> Uses parentheses (e.g., (V) = 5000, (X) = 10000).</summary>
		Parentheses,

		/// <summary> Uses ancient Roman apostrophus notation (|Ↄ|Ↄ = 5000, CC|Ↄ|Ↄ = 10000).</summary>
		Apostrophus,
	}
}
