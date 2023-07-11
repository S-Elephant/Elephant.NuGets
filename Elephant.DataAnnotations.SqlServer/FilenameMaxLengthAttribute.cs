using System.ComponentModel.DataAnnotations;
using Elephant.Constants.SqlServer;

namespace Elephant.DataAnnotations.SqlServer
{
	/// <summary>
	/// Validation attribute that checks if the value is smaller ir equal to <see cref="MaxLength"/> or null.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class FilenameMaxLengthAttribute : MaxLengthAttribute
	{
		/// <summary>
		/// Maximum length.
		/// </summary>
		protected int MaxLength { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public FilenameMaxLengthAttribute(int maxLength = DbLengths.Filename)
			: base(maxLength)
		{
			MaxLength = maxLength;
		}

		/// <summary>
		/// Validate that the <paramref name="value"/> is either null or is equal or smaller than <see cref="MaxLength"/>.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns>True if the length is smaller or equal to <see cref="MaxLength"/> or if it is null.</returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			// Allow null.
			if (value == null)
				return ValidationResult.Success;

			// Process base.
			bool isValid = IsValid(value);

			if (isValid)
				return ValidationResult.Success;

			return new ValidationResult($"Length cannot be more than {MaxLength}. Actual: {value!.ToString().Length}.");
		}
	}
}
