using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations
{
	/// <summary>
	/// Validation attribute to check that the GUID is not a <see cref="Guid.Empty"/>
	/// with an optional null validation.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class GuidNotEmptyAttribute : ValidationAttribute
	{
		/// <summary>
		/// Maximum file size in bytes.
		/// </summary>
		private readonly bool _allowNull;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="allowNull">If true, a null-value will not cause a validation error. Defaults to <c>true</c>.</param>
		public GuidNotEmptyAttribute(bool allowNull = true)
		{
			_allowNull = allowNull;
		}

		/// <summary>
		/// Validate that the file has a file size equal or smaller than the specified maximum (in bytes) or that it is null.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns><see cref="ValidationResult.Success"/> if the <see cref="Guid"/> is not a <see cref="Guid.Empty"/> and depending on the allowNull parameter a null validation will also occur.</returns>
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (ReferenceEquals(value, null)) // Comparing it this way instead of == null, will not cause a bug when someone override the == operator in an unlucky way.
				return _allowNull ? ValidationResult.Success : new ValidationResult("Expected a GUID but got a null-value.");

			if (value is Guid guidValue)
			{
				if (guidValue.Equals(Guid.Empty))
					return new ValidationResult("GUID should not be empty.");

				return ValidationResult.Success;
			}

			return new ValidationResult("Unknown type.");
		}
	}
}
