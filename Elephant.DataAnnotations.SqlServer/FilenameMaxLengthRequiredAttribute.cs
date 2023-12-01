using System.ComponentModel.DataAnnotations;
using Elephant.Constants.SqlServer;

namespace Elephant.DataAnnotations.SqlServer
{
	/// <summary>
	/// Validation attribute that checks if the value is smaller ir equal to <see cref="MaxLength"/> and not null.
	/// and also checks if it is not null.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class FilenameMaxLengthRequiredAttribute : MaxLengthAttribute
	{
		/// <summary>
		/// Maximum length.
		/// </summary>
		protected int MaxLength { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="maxLength">Maximum length.</param>
		public FilenameMaxLengthRequiredAttribute(int maxLength = DbLengths.Filename)
			: base(maxLength)
		{
			MaxLength = maxLength;
		}

		/// <summary>
		/// Validate that the <paramref name="value"/> is not null and is smaller or equal to <see cref="MaxLength"/>.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns>True if the length is smaller or equal to <see cref="MaxLength"/> and if it is not null.</returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			// Disallow null.
			if (value == null)
				return new ValidationResult($"Value cannot be null.");

			// Process base.
			bool isValid = IsValid(value);

			if (isValid)
				return ValidationResult.Success;

			return new ValidationResult($"Length cannot be more than {MaxLength}. Actual: {(value!.ToString() ?? "").Length}.");
		}
	}
}
