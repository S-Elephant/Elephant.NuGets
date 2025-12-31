namespace Elephant.Texts.Abstractions
{
	/// <summary>
	/// Validate strings for balanced parentheses of various types using strict nesting.
	/// </summary>
	/// <example>
	/// <list type="bullet">
	///   <item>
	///     <description><b>Correct</b>: ()[] → Properly nested and closed.</description>
	///   </item>
	///   <item>
	///     <description><b>Incorrect</b>: ([)] → Strict nesting violation (parentheses cross).</description>
	///   </item>
	///   <item>
	///     <description><b>Incorrect</b>: ()] → Unmatched closing bracket (no opening '[' for ']').</description>
	///   </item>
	/// </list>
	/// </example>
	public interface IParenthesesValidator
	{
		/// <summary>
		/// Validate whether all parentheses in the <paramref name="text"/> string are properly balanced
		/// and closed using strict nesting.
		/// </summary>
		/// <param name="text">String to validate for balanced parentheses.</param>
		/// <returns>
		/// True if all parentheses are properly balanced and closed, false otherwise.
		/// Non-parentheses characters are ignored during validation.
		/// </returns>
		bool IsValid(string? text);
	}
}
