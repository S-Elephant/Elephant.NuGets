using System.ComponentModel.DataAnnotations;
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
		private Regex _regex = new (_baseRegex);

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="allowDot">If true, then dots "." are also allowed. Defaults to false.</param>
		/// <param name="allowUnderscore">If true, then underscores "_" are also allowed. Defaults to false.</param>
		public FilenameAllowAlphaNumericOnly(bool allowDot = false, bool allowUnderscore = false)
		{
			string finalRegex = _baseRegex;

			if (allowDot)
				finalRegex = finalRegex.Insert(_baseRegex.Length - 1, ".");

			if (allowUnderscore)
				finalRegex = finalRegex.Insert(_baseRegex.Length - 1, "_");

			_regex = new Regex(finalRegex);
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
