using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Elephant.DataAnnotations
{
	/// <summary>
	/// Validation attribute to check the <see cref="EqualsAnotherPropertyStringAttribute.OtherPropertyName"/> contains EXACTLY the same string as this attribute its property.
	/// Only compares <see cref="object.ToString"/> values (<see cref="StringComparison.InvariantCulture"/>).
	/// Validation fails if either value is null.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class EqualsAnotherPropertyStringRequiredAttribute : EqualsAnotherPropertyStringAttribute
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="otherPropertyName">Name of the other attribute that this attribute its property must be equal to.</param>
		/// <param name="isCaseSensitive">Determines if the comparison is case-sensitive. Default: true.</param>
		public EqualsAnotherPropertyStringRequiredAttribute(string otherPropertyName, bool isCaseSensitive = true)
		: base(otherPropertyName, isCaseSensitive)
		{
		}

		/// <summary>
		/// Return true if neither property is null and if both properties <see cref="object.ToString"/> (<see cref="StringComparison.InvariantCulture"/>) match.
		/// </summary>
		/// <param name="value">Object to validate.</param>
		/// <param name="validationContext"><see cref="ValidationContext"/></param>
		/// <returns>True if neither property is null and if both properties <see cref="object.ToString"/> (<see cref="StringComparison.InvariantCulture"/>) match.</returns>
		/// <exception cref="ArgumentException">Thrown if <see cref="EqualsAnotherPropertyStringAttribute.OtherPropertyName"/> wasn't found on the same model as where this attribute was placed.</exception>
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string? currentValue = value?.ToString();

			if (currentValue == null)
				return new ValidationResult("Value is required.");

			PropertyInfo? property = validationContext.ObjectType.GetProperty(OtherPropertyName);

			if (property == null)
				throw new ArgumentException($"Property with name {OtherPropertyName} not found");

			string? otherValue = property.GetValue(validationContext.ObjectInstance)?.ToString();

			if (otherValue == null)
				return new ValidationResult("Other value is required.");

			if (currentValue.Equals(otherValue, IsCaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
				return ValidationResult.Success;

			return new ValidationResult($"Values do not match. Got \"{currentValue}\" and \"{otherValue}\".");
		}
	}
}
