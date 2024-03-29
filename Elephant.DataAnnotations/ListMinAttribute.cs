﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Elephant.DataAnnotations
{
    /// <summary>
    /// Validation attribute to check the <see cref="IList"/> contains at least the specified amount of items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ListMinAttribute : ValidationAttribute
    {
        /// <summary>
        /// Minimum length.
        /// </summary>
        protected int MinLength { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minLength">Minimum length. Must be equal or greater than 0.</param>
        public ListMinAttribute(int minLength)
        {
            MinLength = Math.Max(0, minLength);
        }

        /// <summary>
        /// Validate that the <see cref="IList"/> contains at least the specified amount of items.
        /// If the value is null and the <see cref="MinLength"/> is 0, then the validation also passes.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        /// <param name="validationContext"><see cref="ValidationContext"/></param>
        /// <returns>True if the minimum length is at least <see cref="MinLength"/> or if it is null.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is IList list)
            {
                if (list.Count < MinLength)
                    return new ValidationResult($"List must contain at least {MinLength} items. Actual: {list.Count}.");

                return ValidationResult.Success;
            }

            return new ValidationResult("Unknown type.");
        }
    }
}
