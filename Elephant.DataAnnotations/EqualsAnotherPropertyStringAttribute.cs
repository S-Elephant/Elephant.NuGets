using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Elephant.DataAnnotations
{
	/// <summary>
	/// Validation attribute to check the <see cref="OtherPropertyName"/> contains EXACTLY the same string as this attribute its property.
	/// Only compares <see cref="object.ToString"/> values (<see cref="StringComparison.InvariantCulture"/>).
	/// Validation also passes if both values are null.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class EqualsAnotherPropertyStringAttribute : ValidationAttribute
	{
		/// <summary>
		/// Indicates if the comparison will be case-sensitive. Default: true.
		/// </summary>
		protected bool IsCaseSensitive { get; set; }

		/// <summary>
		/// Name of other property. This other property must be located on the same model.
		/// </summary>
		protected string OtherPropertyName { get; private set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="otherPropertyName">Name of the other attribute that this attribute its property must be equal to.</param>
		/// <param name="isCaseSensitive">Determines if the comparison is case-sensitive. Default: true.</param>
		public EqualsAnotherPropertyStringAttribute(string otherPropertyName, bool isCaseSensitive = true)
		{
			IsCaseSensitive = isCaseSensitive;
			OtherPropertyName = otherPropertyName;
		}

		/// <summary>
		/// Return true if either both properties are null or if both properties <see cref="object.ToString"/> (<see cref="StringComparison.InvariantCulture"/>) match.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns>True if either both properties are null or if both properties <see cref="object.ToString"/> (<see cref="StringComparison.InvariantCulture"/>) match.</returns>
		/// <exception cref="ArgumentException">Thrown if <see cref="OtherPropertyName"/> wasn't found on the same model as where this attribute was placed.</exception>
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string? currentValue = value?.ToString();

			PropertyInfo? property = validationContext.ObjectType.GetProperty(OtherPropertyName);

			if (property == null)
				throw new ArgumentException($"Property with name {OtherPropertyName} not found");

			string? otherValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

			if ((currentValue == null && otherValue == null) ||
				(currentValue != null && currentValue.Equals(otherValue, IsCaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase)))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult($"Values do not match. Got \"{currentValue}\" and \"{otherValue}\".");
		}
	}
}
