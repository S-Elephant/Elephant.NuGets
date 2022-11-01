using System.ComponentModel.DataAnnotations;

namespace Elephant.ApiControllers.Attributes
{
    /// <summary>
    /// Validation attribute to check the value is numeric and either null or greater than zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validate that the <paramref name="value"/> is numeric and null or greater than zero.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if it is greater than zero or if it is null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is int intValue)
                return intValue < 1 ? new ValidationResult($"Value must be at least 1. Actual: {intValue}.") : ValidationResult.Success;

            if (value is float floatValue)
                return floatValue <= 0 ? new ValidationResult($"Value must be greater than zero. Actual: {floatValue}.") : ValidationResult.Success;

            if (value is decimal decimalValue)
            {
                return decimalValue <= 0
                    ? new ValidationResult($"Value must be greater than zero. Actual: {decimalValue}.")
                    : ValidationResult.Success;
            }

            if (value is uint uIntValue)
                return uIntValue == 0 ? new ValidationResult($"Value must be at least 1. Actual: {uIntValue}.") : ValidationResult.Success;

            if (value is nuint nuIntValue)
                return nuIntValue < 1 ? new ValidationResult($"Value must be at least 1. Actual: {nuIntValue}.") : ValidationResult.Success;

            if (value is short shortValue)
                return shortValue <= 0 ? new ValidationResult($"Value must be greater than zero. Actual: {shortValue}.") : ValidationResult.Success;

            if (value is ushort uShortValue)
                return uShortValue == 0 ? new ValidationResult($"Value must be greater than zero. Actual: {uShortValue}.") : ValidationResult.Success;

            if (value is long longValue)
                return longValue <= 0 ? new ValidationResult($"Value must be greater than zero. Actual: {longValue}.") : ValidationResult.Success;

            if (value is ulong uLongValue)
                return uLongValue == 0 ? new ValidationResult($"Value must be greater than zero. Actual: {uLongValue}.") : ValidationResult.Success;

            if (value is byte byteValue)
                return byteValue == 0 ? new ValidationResult($"Value must be at least 1. Actual: {byteValue}.") : ValidationResult.Success;

            if (value is sbyte sByteValue)
                return sByteValue < 1 ? new ValidationResult($"Value must be greater than zero. Actual: {sByteValue}.") : ValidationResult.Success;

            return new ValidationResult("Value is not numeric or is unknown.");
        }
    }
}
