using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Elephant.DataAnnotations
{
	/// <summary>
	/// Validation attribute to check if the <see cref="string"/> or <see cref="int"/> or <see cref="sbyte"/>
	/// or <see cref="short"/> or <see cref="long"/> or <see cref="byte"/>
	/// is either null or only contains alphanumeric (= the numbers 0-9 and letters A-Z (both uppercase and lowercase)) characters
	/// and the numeric value (if any) must be equal or greater than zero.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class FilenameAllowAlphaNumericOnly : ValidationAttribute
	{
		private const string _baseRegex = @"[^a-zA-Z0-9]";
		private Regex _regex = new(_baseRegex);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="allowDot">If true, then dots "." are also allowed. Defaults to false.</param>
		/// <param name="allowUnderscore">If true, then underscores "_" are also allowed. Defaults to false.</param>
		/// <param name="extraAllowedCharacters">If not empty, then any character (case-sensitive) in this string will be allowed as well.</param>
		public FilenameAllowAlphaNumericOnly(bool allowDot = false, bool allowUnderscore = false, string extraAllowedCharacters = "")
		{
			StringBuilder finalRegex = new(_baseRegex);

			if (allowDot)
				finalRegex = finalRegex.Insert(_baseRegex.Length - 1, ".");

			if (allowUnderscore)
				finalRegex = finalRegex.Insert(_baseRegex.Length - 1, "_");

			if (extraAllowedCharacters != string.Empty)
			{
				/* Yeah... This must be escapped manually because Regex.Escape(..) does not escape certain cases like "{}" for example and
				 * because this goes between the brackets [], this must be escaped...
				 * For more info see: https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.escape?redirectedfrom=MSDN&view=net-7.0#System_Text_RegularExpressions_Regex_Escape_System_String_
				 * Quote:
				 * "While the Escape method escapes the straight opening bracket ([) and opening brace ({) characters, it does not escape their corresponding
				 * closing characters (] and }). In most cases, escaping these is not necessary. If a closing bracket or brace is not preceded by its
				 * corresponding opening character, the regular expression engine interprets it literally. If an opening bracket or brace is interpreted
				 * as a metacharacter, the regular expression engine interprets the first corresponding closing character as a metacharacter. If this is not
				 * the desired behavior, the closing bracket or brace should be escaped by explicitly prepending the backslash (\) character."
				 */
				StringBuilder escapedExtraAllowedCharacters = new();
				foreach (char extraAllowedCharacter in extraAllowedCharacters)
					escapedExtraAllowedCharacters.Append($"\\{extraAllowedCharacter}");

				finalRegex = finalRegex.Insert(_baseRegex.Length - 1, escapedExtraAllowedCharacters);
			}

			_regex = new Regex(finalRegex.ToString());
		}

		/// <summary>
		/// Validate that the <paramref name="value"/> is either null or only contains alphanumeric (= the numbers 0-9 and letters A-Z (both uppercase and lowercase)) characters
		/// and the numeric value (if any) must be equal or greater than zero.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns>True if the <paramref name="value"/> is null or contains only alphanumeric characters and the numeric value (if any) must be equal or greater than zero..</returns>
		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
#pragma warning disable CS8603 // Possible null reference return.
			// Allow null.
			if (value == null)
				return ValidationResult.Success;

			if (value is string stringValue)
				return _regex.IsMatch(stringValue) ? new ValidationResult($"Value may only contains alphanumerics (and if pure numeric, then positive whole numbers only). Actual: {stringValue}.") : ValidationResult.Success;

			if (value is byte || value is uint || value is ulong)
				return ValidationResult.Success;

			if (value is int intValue)
				return intValue < 0 ? new ValidationResult("Value must be equal or greater than 0.") : ValidationResult.Success;

			if (value is sbyte sbyteValue)
				return sbyteValue < 0 ? new ValidationResult("Value must be equal or greater than 0.") : ValidationResult.Success;

			if (value is short shortValue)
				return shortValue < 0 ? new ValidationResult("Value must be equal or greater than 0.") : ValidationResult.Success;

			if (value is long longValue)
				return longValue < 0 ? new ValidationResult("Value must be equal or greater than 0.") : ValidationResult.Success;

			return new ValidationResult($"Unknown type. {nameof(PathFolderMaxLengthAttribute)} is only allowed on type of string.");
#pragma warning restore CS8603 // Possible null reference return.
		}
	}
}
